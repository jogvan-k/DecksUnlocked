using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;

namespace KeyforgeUnlocked
{
  public class Deck
  {
    public static Deck Empty => new Deck(new List<Card>());
    public ImmutableList<Card> Cards { get; }

    public Deck(List<Card> cards)
    {
      Cards = cards.ToImmutableList();
    }

    public static Deck LoadDeck()
    {
      return new Deck(Enumerable.Range(0, 36).Select(i => (Card) new SimpleCreatureCard()).ToList());
    }

  }
}