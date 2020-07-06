using System;
using System.Linq;
using CommandLine;
using KeyforgeUnlocked;

namespace KeyforgeUnlockedConsole
{
  class ConsoleGame
  {
    static readonly Parser parser = new Parser();

    static void Main(string[] args)
    {
      State state = StateFactory.Initiate(Deck.LoadDeck(), Deck.LoadDeck());
      while (!state.IsGameOver)
      {
        state.Print();
        state.PrintActions();
        var i = Int16.Parse(Console.ReadLine());
        state = (State) state.Actions[i].DoCoreAction();
      }
    }
  }
}