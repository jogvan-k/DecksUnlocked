using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using KeyforgeUnlockedConsole.PrintCommands;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlockedConsole
{
  public class ConsoleGame
  {
    IState _state;
    IDictionary<string, IActionGroup> Commands;
    IDictionary<string, IPrintCommand> HelperCommands = PrintCommandsFactory.HelperCommands;

    public ConsoleGame(IState state)
    {
      _state = state;
    }

    public void StartGame()
    {
      while (!_state.IsGameOver)
      {
        Console.Clear();
        _state.Print(out var commands);
        Commands = commands;

        var command = ReadCommand();
        if (int.TryParse(command, out var i))
        {
          _state = Commands["action"].Actions[i - 1].DoAction(_state);
        }
        else
        {
          ResolveCommand(Commands[command]);
        }
      }

      Console.Clear();
      _state.Print(out _);
      Console.WriteLine($"{_state.PlayerTurn} won!");
      Console.WriteLine();
    }

    string ReadCommand()
    {
      while (true)
      {
        Console.Write("Action: ");
        var command = Console.ReadLine().ToLower();
        if (Commands.Keys.Contains("action"))
        {
          if (int.TryParse(command, out var i) && 0 < i && i <= Commands["action"].Actions.Count)
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
      var actions = command.Actions;
      if (actions.Count == 0)
        throw new InvalidOperationException("List /'IActionGroup.Actions/' must not be empty ");
      if (actions.Count == 1)
      {
        _state = command.Actions.Single().DoAction(_state);
        return;
      }

      var action = WriteAndReadActions(command);
      _state = action.DoAction(_state);
    }

    Action WriteAndReadActions(IActionGroup actionGroup)
    {
      Console.WriteLine(actionGroup.ToConsole());

      int i = 1;
      var actions = new List<Action>();
      foreach (var action in actionGroup.Actions)
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