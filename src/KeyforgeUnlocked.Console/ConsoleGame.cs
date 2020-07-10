using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.ActionGroup;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlockedConsole
{
  static class ConsoleGame
  {
    static void Main(string[] args)
    {
      IState state = StateFactory.Initiate(Deck.LoadDeck(), Deck.LoadDeck());
      while (!state.IsGameOver)
      {
        state.Print(out var commands);

        var command = ReadCommand(commands);
        state = ResolveCommand(state, commands[command]);
      }
    }

    static string ReadCommand(Dictionary<string, IActionGroup> commands)
    {
      while (true)
      {
        Console.Write("Action: ");
        var command = Console.ReadLine();
        if (commands.Keys.Contains(command))
          return command;
        Console.WriteLine($"Invalid command: {command}");
      }
    }

    static IState ResolveCommand(IState state,
      IActionGroup command)
    {
      var actions = command.Actions;
      if (actions.IsEmpty)
        throw new InvalidOperationException("List /'IActionGroup.Actions/' must not be empty ");
      if (actions.Count == 1)
        return command.Actions.Single().DoAction(state);
      var action = WriteAndReadActions(command);
      return action.DoAction(state);
    }

    static Action WriteAndReadActions(IActionGroup actionGroup)
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