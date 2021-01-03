using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using KeyforgeUnlockedConsole.PrintCommands;
using UnlockedCore;
using UnlockedCore.AITypes;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
  public abstract class BaseConsoleGame : IConsoleGame
  {
    readonly LogInfo _logInfo;
    protected IState _state;
    protected IDictionary<string, IActionGroup> Commands;
    protected IDictionary<string, IPrintCommand> HelperCommands = PrintCommandsFactory.HelperCommands;
    protected Stack<IState> previousStates = new();

    public BaseConsoleGame(IState state, LogInfo logInfo = LogInfo.None)
    {
      _logInfo = logInfo;
      _state = state;
      Commands = new Dictionary<string, IActionGroup>();
    }

    public void StartGame()
    {
      PrintStatus();
      while (!_state.IsGameOver)
      {
        AdvanceState();
      }

      Console.Clear();
      _state.Print(_logInfo, out _);
      Console.WriteLine($"{_state.PlayerTurn} won!");
      Console.WriteLine();
    }

    protected abstract void AdvanceState();

    protected void AdvanceStateOnPlayerTurn()
    {
      var command = ReadCommand();
      if (command == "undo")
      {
        if (previousStates.Count > 0)
          _state = previousStates.Pop();

        PrintStatus();
      }
      else if (command == "clear" || command == "")
      {
        PrintStatus();
      }
      else if (int.TryParse(command, out var i))
      {
        previousStates.Push(_state);
        _state = Commands["action"].Actions(_state.ToImmutable())[i - 1].DoAction(_state);
        PrintStatus();
      }
      else if(Commands.Keys.Contains(command))
      {
        ResolveCommand(Commands[command]);
        PrintStatus();
      } else
      {
        var card = _state.GetCard(command);
        PrintCardInfo(card!);
      }
    }

    void PrintStatus()
    {
      Console.Clear();
      _state.Print(_logInfo, out var commands);
      Commands = commands;
    }

    void PrintCardInfo(ICard card)
    {
      Console.WriteLine();
      Console.WriteLine($"{card.Name} - {card.House}");
      if (card is ICreatureCard creature)
      {
        PrintCardInfo(creature);
      } else if (card is IActionCard action)
      {
        Console.WriteLine("Action");
      } else if (card is IArtifactCard artifact)
      {
        Console.WriteLine("Artifact");
      }
      
      Console.WriteLine();
    }

    void PrintCardInfo(ICreatureCard creature)
    {
      Console.WriteLine("Creature");
      Console.WriteLine(ReadableTraits(creature.CardTraits));
      var armor = creature.CardArmor > 0 ? creature.CardArmor.ToString() : "~";
      Console.WriteLine($"Power: {creature.CardPower}, Armor: {armor}");
    }
    
    void PrintCardInfo(IActionCard action)
    {
      Console.WriteLine("Action");
    }
    
    void PrintCardInfo(IArtifactCard artifact)
    {
      Console.WriteLine("Artifact");
      Console.WriteLine(ReadableTraits(artifact.CardTraits));
    }

    string ReadableTraits(Trait[] traits)
    {
      return traits.Select(t => t.ToString()).Aggregate((f, s) => $"{f}, {s}");;
    }

    protected void AdvanceStateOnAITurn(Player aiPlayer, IGameAI gameAi)
    {
      var aiMoves = Array.Empty<int>();
      while (_state.PlayerTurn == aiPlayer)
      {
        var aiLogString = "";
        if (aiMoves.Length == 0)
        {
          _state.PrintAITurn(_logInfo);
          Console.WriteLine("Calculating moves...");
          aiMoves = gameAi.DetermineAction(_state);
          if (_logInfo == LogInfo.CalculationInfo && gameAi is NegamaxAI negamaxAi)
          {
            if (negamaxAi.LatestLogInfo.nodesEvaluated > 0 && negamaxAi.LatestLogInfo.elapsedTime.Ticks > 0)
              aiLogString =
                $"{negamaxAi.LatestLogInfo.nodesEvaluated} states evaluated in {negamaxAi.LatestLogInfo.elapsedTime.TotalSeconds} seconds. \n" +
                $"{negamaxAi.LatestLogInfo.successfulHashMapLookups} successful hash map lookups.";
          }
        }

        _state = (IState) _state.Actions()[aiMoves[0]].DoCoreAction();
        aiMoves = aiMoves.Skip(1).ToArray();
        _state.PrintAITurn(_logInfo);

        if (!string.IsNullOrEmpty(aiLogString))
          Console.WriteLine(aiLogString);
        if (_state.PlayerTurn == aiPlayer)
        {
          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
          Console.Clear();
        }
      }
    }

    string ReadCommand()
    {
      while (true)
      {
        Console.Write("Action: ");
        var command = Console.ReadLine()?.ToLower().Replace(' ', default);
        if (command == null)
          return "";
        if (Commands.Keys.Contains("action"))
        {
          if (int.TryParse(command, out var i) && 0 < i && i <= Commands["action"].Actions(_state.ToImmutable()).Count)
            return command;
        }
        else if (HelperCommands.Keys.Contains(command))
          HelperCommands[command].Print(_state);
        else if (Commands.Keys.Contains(command))
          return command;
        else if (command == "undo" || command == "clear" || _state.GetCard(command) != null)
          return command;
        else
          Console.WriteLine($"Invalid command: {command}");
      }
    }

    void ResolveCommand(IActionGroup command)
    {
      var actions = command.Actions(_state.ToImmutable());
      if (actions.Count == 0)
        throw new InvalidOperationException("List /'IActionGroup.Actions/' must not be empty ");
      if (actions.Count == 1)
      {
        previousStates.Push(_state);
        _state = actions.Single().DoAction(_state);
        return;
      }

      var action = WriteAndReadActions(command);
      previousStates.Push(_state);
      _state = action.DoAction(_state);
    }

    IAction WriteAndReadActions(IActionGroup actionGroup)
    {
      Console.WriteLine(actionGroup.ToConsole());

      int i = 1;
      var actions = new List<IAction>();
      foreach (var action in actionGroup.Actions(_state.ToImmutable()))
      {
        Console.WriteLine($"{i++}: {action.ToConsole()}");
        actions.Add(action);
      }

      while (true)
      {
        Console.Write("Action: ");
        var action = Console.ReadLine();
        if (Int16.TryParse(action, out var a) && InRange(a, 1, i - 1))
        {
          return actions[a - 1];
        }

        Console.WriteLine($"Invalid input {action}");
      }
    }

    static bool InRange(short candidate,
      int from,
      int to)
    {
      return from <= candidate && candidate <= to;
    }
  }
}