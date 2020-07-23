using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;

namespace KeyforgeUnlocked.Types
{
  public class Deck
  {
    static Lazy<IEnumerable<Type>> allCards = new Lazy<IEnumerable<Type>>(GetAllCards);
    public static IEnumerable<Type> AllCards => allCards.Value;
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
        var cardType = AllCards.Single(t => IsCard(t, cardString));
        cards.Add((Card) cardType.GetConstructor(new Type[0]).Invoke(new object[0]));
      }

      return new Deck(cards);
    }

    static bool IsCard(Type cardType, string cardString)
    {
      var fieldInfo = cardType.GetField("SpecialName", BindingFlags.Public | BindingFlags.Static);
      if (fieldInfo == null)
        return cardType.Name.Equals(cardString);
      var name = (string) fieldInfo.GetValue(null);
      return cardString.Equals(name);
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