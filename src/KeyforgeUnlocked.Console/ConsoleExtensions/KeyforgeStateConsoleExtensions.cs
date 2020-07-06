using System;
using System.Linq;
using KeyforgeUnlocked;

namespace KeyforgeUnlockedConsole
{
  public static class KeyforgeStateConsoleExtensions
  {
    public static void PrintActions(this State state)
    {
      int i = 0;
      Console.WriteLine("Possible actions:");
      foreach (var action in state.Actions)
      {
        Console.WriteLine($"[{i++}]: {action.ToConsole()}.");
      }
    }

    public static void Print(this State state)
    {
      Console.WriteLine($"Current turn: {state.PlayerTurn}");
      Console.WriteLine($"Cards in hand:");
      var currentPlayer = state.PlayerTurn;
      foreach (var card in state.Hands[currentPlayer].OrderBy(c => c.House))
      {
        Console.WriteLine(card);
      }
    }
  }
}