
using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using KeyforgeUnlockedConsole.PrintCommands;
using UnlockedCore;
using UnlockedCore.AITypes;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
  public abstract class BaseConsoleGame : IConsoleGame
  {
    protected IState _state;
    protected IDictionary<string, IActionGroup> Commands;
    protected IDictionary<string, IPrintCommand> HelperCommands = PrintCommandsFactory.HelperCommands;
    protected IEvaluator _evaluator = new Evaluator();

    public BaseConsoleGame(IState state)
    {
      _state = state;
    }

    public void StartGame()
    {
      while (!_state.IsGameOver)
      {
        AdvanceState();
      }

      Console.Clear();
      _state.Print(out _);
      Console.WriteLine($"{_state.PlayerTurn} won!");
      Console.WriteLine();
    }

    protected abstract void AdvanceState();

    protected void AdvanceStateOnPlayerTurn()
    {
      Console.Clear();
      _state.Print(out var commands);
      Commands = commands;

      var command = ReadCommand();
      if (int.TryParse(command, out var i))
      {
        _state = Commands["action"].Actions(_state.ToImmutable())[i - 1].DoAction(_state);
      }
      else
      {
        ResolveCommand(Commands[command]);
      }
    }


    protected void AdvanceStateOnAITurn(Player aiPlayer, IGameAI gameAi)
    {
      var aiMoves = Array.Empty<int>();
      while (_state.PlayerTurn == aiPlayer)
      {
        var aiLogString = "";
        if (aiMoves.Length == 0)
        {
          aiMoves = gameAi.DetermineAction(_state);
          if (gameAi is NegamaxAI negamaxAi)
          {
            if (negamaxAi.LatestLogInfo.nodesEvaluated > 0 && negamaxAi.LatestLogInfo.elapsedTime.Ticks > 0)
              aiLogString =
                $"{negamaxAi.LatestLogInfo.nodesEvaluated} states evaluated in {negamaxAi.LatestLogInfo.elapsedTime.TotalSeconds} seconds. \n" +
                $"{negamaxAi.LatestLogInfo.successfulHashMapLookups} successful hash map lookups.";
          }
        }

        _state = (IState) _state.Actions()[aiMoves[0]].DoCoreAction();
        aiMoves = aiMoves.Skip(1).ToArray();
        _state.PrintAITurn();

        if (!string.IsNullOrEmpty(aiLogString))
          Console.WriteLine(aiLogString);
        if (_state.PlayerTurn == aiPlayer)
        {
          Console.WriteLine("Press any key to continue...");
          Console.ReadLine();
          Console.Clear();
        }
      }
    }

    string ReadCommand()
    {
      while (true)
      {
        Console.Write("Action: ");
        var command = Console.ReadLine().ToLower();
        if (Commands.Keys.Contains("action"))
        {
          if (int.TryParse(command, out var i) && 0 < i && i <= Commands["action"].Actions(_state.ToImmutable()).Count)
            return command;
        }
        else if (HelperCommands.Keys.Contains(command))
          HelperCommands[command].Print(_state);
        else if (Commands.Keys.Contains(command))
          return command;

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
        _state = actions.Single().DoAction(_state);
        return;
      }

      var action = WriteAndReadActions(command);
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