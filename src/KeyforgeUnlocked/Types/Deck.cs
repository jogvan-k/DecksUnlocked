using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;

namespace KeyforgeUnlocked
{
  public class Deck
  {
    public static Lazy<IEnumerable<Type>> AllCards = new Lazy<IEnumerable<Type>>(() => GetAllCards());
    public static Deck Empty => new Deck(new List<Card>());
    public ImmutableList<Card> Cards { get; }

    public Deck(IEnumerable<Card> cards)
    {
      Cards = cards.ToImmutableList();
    }

    public static Deck LoadDeck()
    {
      return new Deck(
        Enumerable.Range(0, 12).SelectMany(
            i => new[] {(Card) new LogosCreatureCard(), new StarAllianceCreatureCard(), new UntamedCreatureCard()})
          .ToList());
    }

    public static Deck LoadDeckFromFile(string filename)
    {
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Decks", "Sample.txt");
      var deckString = System.IO.File.ReadLines(path);
      var cards = new List<Card>();
      foreach (var cardString in deckString)
      {
        var cardType = AllCards.Value.Single(t => t.Name == cardString);
        cards.Add((Card) cardType.GetConstructor(new Type[0]).Invoke(new object[0]));
      }

      return new Deck(cards);
    }

    static IEnumerable<Type> GetAllCards()
    {
      var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "KeyforgeUnlocked");
      return from type in assembly.GetTypes()
        where typeof(Card).IsAssignableFrom(type)
        select type;
    }
  }
}