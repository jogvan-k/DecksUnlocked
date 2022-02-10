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
        public static Deck Empty => new Deck(new List<ICard>());
        public ImmutableList<ICard> Cards { get; }

        public Deck(IEnumerable<ICard> cards)
        {
            Cards = cards.ToImmutableList();
        }

        public static Deck LoadDeckFromFile(Assembly assembly, string filename)
        {
            var pathToDecks = Directory.GetDirectories(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."), "Decks",
                SearchOption.AllDirectories);
            var deckString = File.ReadLines(Path.Combine(pathToDecks.First(), filename));
            var cardsDictionary = CardsDictionary(assembly);
            var cards = new List<Card>();
            foreach (var cardString in deckString)
            {
                var card = cardsDictionary[cardString].GetConstructor(new Type[0])?.Invoke(new object[0]);
                if (card != null)
                    cards.Add((Card)card);
            }

            return new Deck(cards);
        }

        static IDictionary<string, Type> CardsDictionary(Assembly assembly)
        {
            var allCardTypes = from type in assembly.GetTypes()
                where typeof(Card).IsAssignableFrom(type)
                select type;

            return allCardTypes.ToDictionary(t => Card.GetName(t), t => t);
        }
    }
}