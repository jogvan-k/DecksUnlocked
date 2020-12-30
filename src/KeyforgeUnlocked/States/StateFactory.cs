using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

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

      var metadata = new Metadata(initialDecks, houses, 40);

      var effects = new LazyStackQueue<IEffect>(new[] {(IEffect) new InitiateGame()});

      return new MutableState(
          Player.Player1,
          1,
          false,
          null,
          EmptyValues(),
          EmptyValues(),
          new LazyList<IActionGroup>(),
          decks,
          EmptySet(),
          EmptySet(),
          EmptySet(),
          EmptyField(),
          EmptyArtifacts(),
          effects,
          new LazyEvents(),
          new LazyList<IResolvedEffect>(),
          new ImmutableHistoricData().ToMutable(),
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

    static LookupReadOnly<Player, IMutableStackQueue<ICard>> ToDecks(Deck player1Deck,
      Deck player2Deck)
    {
      return new(new Dictionary<Player, IMutableStackQueue<ICard>>
      {
        {Player.Player1, new LazyStackQueue<ICard>(player1Deck.Cards)},
        {Player.Player2, new LazyStackQueue<ICard>(player2Deck.Cards)}
      });
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

    static Types.Lookup<Player, int> EmptyValues()
    {
      return new(new Dictionary<Player, int>
      {
        {Player.Player1, 0}, {Player.Player2, 0}
      });
    }

    static LookupReadOnly<Player, IMutableSet<ICard>> EmptySet()
    {
      return new(new Dictionary<Player, IMutableSet<ICard>>()
      {
        {Player.Player1, new LazySet<ICard>()}, {Player.Player2, new LazySet<ICard>()}
      });
    }

    static LookupReadOnly<Player, IMutableList<Creature>> EmptyField()
    {
      return new(new Dictionary<Player, IMutableList<Creature>>
      {
        {Player.Player1, new LazyList<Creature>()}, {Player.Player2, new LazyList<Creature>()}
      });
    }

    static LookupReadOnly<Player, IMutableSet<Artifact>> EmptyArtifacts()
    {
      return new(new Dictionary<Player, IMutableSet<Artifact>>
      {
        {Player.Player1, new LazySet<Artifact>()}, {Player.Player2, new LazySet<Artifact>()}
      });
    }
  }
}