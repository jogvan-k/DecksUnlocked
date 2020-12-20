using System.Reflection;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedConsole.Decks
{
  public static class DeckLoader
  {
    public static Deck LoadDeck(string filename)
    {
      return Deck.LoadDeckFromFile(Assembly.Load("KeyforgeUnlocked.Cards"),filename);
    }
  }
}