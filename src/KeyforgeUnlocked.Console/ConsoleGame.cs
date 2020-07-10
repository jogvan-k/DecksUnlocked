using System;
using System.Linq;
using CommandLine;
using KeyforgeUnlocked;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedConsole
{
  static class ConsoleGame
  {
    static readonly Parser parser = new Parser();

    static void Main(string[] args)
    {
      IState state = StateFactory.Initiate(Deck.LoadDeck(), Deck.LoadDeck());
      while (!state.IsGameOver)
      {
        state.Print();
        state.PrintActions();
        var i = Int16.Parse(Console.ReadLine());
        state = (IState) state.Actions[i].DoCoreAction(state);
      }
    }
  }
}