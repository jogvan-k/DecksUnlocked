using KeyforgeUnlocked;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedConsole
{
  public static class Entry
  {
    static void Main(string[] args)
    {
      var consoleGame = new ConsoleGame(StateFactory.Initiate(Deck.LoadDeck(), Deck.LoadDeck()));
      consoleGame.StartGame();
    }
  }
}