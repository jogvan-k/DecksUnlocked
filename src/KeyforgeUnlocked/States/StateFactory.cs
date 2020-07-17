using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateFactory
  {
    public static ImmutableState Initiate(Deck player1Deck,
      Deck player2Deck)
    {
      var decks = ToDecks(player1Deck, player2Deck);

      var initialDecks = ToInitialDecks(player1Deck, player2Deck);
      var houses = ToHouses(player1Deck, player2Deck);

      var metadata = new Metadata(initialDecks, houses);

      var effects = new StackQueue<IEffect>(new[] {(IEffect) new InitiateGame()});

      return new MutableState(
          Player.Player1,
          1,
          false,
          null,
          null,
          EmptyValues(),
          EmptyValues(),
          new List<IActionGroup>(),
          decks,
          EmptySet(),
          EmptySet(),
          EmptySet(),
          EmptyField(),
          effects,
          new List<IResolvedEffect>(),
          metadata)
        .ResolveEffects();
    }

    static ImmutableDictionary<Player, IImmutableSet<House>> ToHouses(Deck player1Deck,
      Deck player2Deck)
    {
      var player1Houses = ToHouseSet(player1Deck);
      var player2Houses = ToHouseSet(player2Deck);
      return ImmutableDictionary.Create<Player, IImmutableSet<House>>().AddRange(
        new[]
        {
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player1, player1Houses),
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player2, player2Houses),
        });
    }

    static ImmutableHashSet<House> ToHouseSet(Deck player1Deck)
    {
      return player1Deck.Cards.Select(c => c.House).Distinct().ToImmutableHashSet();
    }

    static Dictionary<Player, Stack<Card>> ToDecks(Deck player1Deck,
      Deck player2Deck)
    {
      return new Dictionary<Player, Stack<Card>>
      {
        {Player.Player1, new Stack<Card>(player1Deck.Cards)},
        {Player.Player2, new Stack<Card>(player2Deck.Cards)}
      };
    }

    static ImmutableDictionary<Player, Deck> ToInitialDecks(Deck player1Deck,
      Deck player2Deck)
    {
      return ImmutableDictionary<Player, Deck>.Empty.AddRange(
        new[]
        {
          new KeyValuePair<Player, Deck>(Player.Player1, player1Deck),
          new KeyValuePair<Player, Deck>(Player.Player2, player2Deck)
        });
    }

    static Dictionary<Player, int> EmptyValues()
    {
      return new Dictionary<Player, int>
      {
        {Player.Player1, 0}, {Player.Player2, 0}
      };
    }

    static Dictionary<Player, IList<Card>> EmptyDeck<T>()
    {
      return new Dictionary<Player, IList<Card>>
      {
        {Player.Player1, new List<Card>()}, {Player.Player2, new List<Card>()}
      };
    }

    static Dictionary<Player, ISet<Card>> EmptySet()
    {
      return new Dictionary<Player, ISet<Card>>
      {
        {Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}
      };
    }

    static Dictionary<Player, IList<Creature>> EmptyField()
    {
      return new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}
      };
    }
  }
}