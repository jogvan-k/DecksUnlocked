using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Types
{
  public class Deck
  {
    public static Deck Empty => new Deck(new List<Card>());
    public ImmutableList<Card> Cards { get; }

    public Deck(IEnumerable<Card> cards)
    {
      Cards = cards.ToImmutableList();
    }

    public static Deck LoadDeckFromFile(Assembly assembly, string filename)
    {
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Decks", filename);
      var deckString = File.ReadLines(path);
      var cardsDictionary = CardsDictionary(assembly);
      var cards = new List<Card>();
      foreach (var cardString in deckString)
      {
        var card = (Card) cardsDictionary[cardString].GetConstructor(new Type[0]).Invoke(new object[0]);
        cards.Add(card);
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

    static IDictionary<string, Type> CardsDictionary(Assembly assembly)
    {
      var allCardTypes =  from type in assembly.GetTypes()
        where typeof(Card).IsAssignableFrom(type)
        select type;

      return allCardTypes.ToDictionary(t => Card.GetName(t), t => t);
    }
  }
}