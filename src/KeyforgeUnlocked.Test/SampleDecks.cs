using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;

namespace KeyforgeUnlockedTest
{
  public class SampleDecks
  {
    static Card[] simpleDeck = Enumerable.Range(1, 36).Select(i => new SimpleCreatureCard()).ToArray();

    public static Stack<Card> SimpleDeck => new Stack<Card>(simpleDeck);
  }
}