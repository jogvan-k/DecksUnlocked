using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;

namespace KeyforgeUnlocked
{
  public class Deck
  {
    public List<Card> Cards { get; }

    public Deck(List<Card> cards)
    {
      Cards = cards;
    }

    public static Deck LoadDeck()
    {
      return new Deck(Enumerable.Repeat((Card)new SimpleCreatureCard(), 32).ToList());
    }
  }
}