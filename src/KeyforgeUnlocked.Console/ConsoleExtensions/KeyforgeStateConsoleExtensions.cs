using System;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedConsole
{
  public static class KeyforgeStateConsoleExtensions
  {
    public static void PrintActions(this IState state)
    {
      int i = 0;
      Console.WriteLine("Possible actions:");
      foreach (var action in state.Actions)
      {
        Console.WriteLine($"[{i++}]: {action.ToConsole()}.");
      }
    }

    public static void Print(this IState state)
    {
      var playerTurn = state.PlayerTurn;
      Console.WriteLine($"Current turn: {playerTurn}");
      Console.WriteLine($"Turn number: {state.TurnNumber}");
      Console.WriteLine($"Cards in hand: ");
      var hand = state.Hands[playerTurn].ToList();
      var handCount = hand.Count;
      for (var i = 0; i < handCount; i++)
      {
        if(i != 0)
          Console.Write(", ");
        Console.Write(hand[i]);
      }
      Console.WriteLine();
    }
  }
}