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
    IState State;
    IDictionary<string, IActionGroup> Commands;
    IDictionary<string, IPrintCommand> HelperCommands = PrintCommandsFactory.HelperCommands;

    public ConsoleGame(IState state)
    {
      State = state;
    }

    public void StartGame()
    {
      while (!State.IsGameOver)
      {
        Console.Clear();
        State.Print(out var commands);
        Commands = commands;

        var command = ReadCommand();
        ResolveCommand(Commands[command]);
      }
    }

    string ReadCommand()
    {
      while (true)
      {
        Console.Write("Action: ");
        var command = Console.ReadLine().ToLower();
        if (HelperCommands.Keys.Contains(command))
          HelperCommands[command].Print(State);
        else if (Commands.Keys.Contains(command))
          return command;
        else
          Console.WriteLine($"Invalid command: {command}");
      }
    }

    void ResolveCommand(IActionGroup command)
    {
      var actions = command.Actions;
      if (actions.IsEmpty)
        throw new InvalidOperationException("List /'IActionGroup.Actions/' must not be empty ");
      if (actions.Count == 1)
      {
        State = command.Actions.Single().DoAction(State);
        return;
      }

      var action = WriteAndReadActions(command);
      State = action.DoAction(State);
    }

    Action WriteAndReadActions(IActionGroup actionGroup)
    {
      Console.WriteLine(actionGroup.ToConsole());

      int i = 1;
      foreach (var action in actionGroup.Actions)
      {
        Console.WriteLine($"{i++}: {action.ToConsole()}");
      }

      while (true)
      {
        Console.Write("Action: ");
        var action = Console.ReadLine();
        if (Int16.TryParse(action, out var a) && InRange(a, 1, i - 1))
        {
          return actionGroup.Actions[a - 1];
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