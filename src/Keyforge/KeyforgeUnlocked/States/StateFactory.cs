using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Algorithms;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;
using static KeyforgeUnlocked.Types.Initializers;

namespace KeyforgeUnlocked.States
{
  public static class StateFactory
  {
    public static ImmutableState Initiate(Deck player1Deck,
      Deck player2Deck,
      int? seed = null)
    {
      var rngSeed = seed ?? new Random().Next();
      var decks = ToDecks(player1Deck, player2Deck, rngSeed);

      var initialDecks = ToInitialDecks(player1Deck, player2Deck);
      var houses = ToHouses(player1Deck, player2Deck);

      var metadata = new Metadata(initialDecks.ToReadOnly(kv => (IImmutableList<ICard>)kv.Value.Cards), houses.ToReadOnly(), 40, rngSeed);

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

    static ImmutableLookup<Player, IMutableStackQueue<ICard>> ToDecks(Deck player1Deck,
      Deck player2Deck, int seed)
    {
      return new(new Dictionary<Player, IMutableStackQueue<ICard>>
      {
        {Player.Player1, new LazyStackQueue<ICard>(player1Deck.Cards)},//Shuffler.Shuffle(player1Deck.Cards, seed))},
        {Player.Player2, new LazyStackQueue<ICard>(player2Deck.Cards)}//Shuffler.Shuffle(player2Deck.Cards, 2 * seed))}
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
  }
}