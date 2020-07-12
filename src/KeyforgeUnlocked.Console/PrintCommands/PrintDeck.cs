using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.PrintCommands;

namespace KeyforgeUnlockedConsole
{
  public class PrintDeck : IPrintCommand
  {
    public void Print(IState state)
    {
      Console.WriteLine("Cards in deck:");
      foreach (var card in state.Decks[state.PlayerTurn])
      {
        Console.WriteLine(card.Name);
      }
    }
  }
}