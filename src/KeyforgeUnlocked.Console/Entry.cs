using KeyforgeUnlocked;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedConsole
{
  public static class Entry
  {
    static void Main(string[] args)
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      var consoleGame = new ConsoleGame(StateFactory.Initiate(player1Deck, player2Deck));
      consoleGame.StartGame();
    }
  }
}