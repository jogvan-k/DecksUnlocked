using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Util
{
  public static class StateTestUtil
  {
    public static MutableState EmptyMutableState => new MutableState(
      Player.Player1,
      0,
      false,
      null,
      null,
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToLookup(),
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToLookup(),
      new LazyList<IActionGroup>(),
      new Dictionary<Player, IMutableStackQueue<Card>> {{Player.Player1, new LazyStackQueue<Card>()}, {Player.Player2, new LazyStackQueue<Card>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableList<Creature>>
        {{Player.Player1, new LazyList<Creature>()}, {Player.Player2, new LazyList<Creature>()}}.ToReadOnly(),
      new LazyStackQueue<IEffect>(),
      new LazyList<IResolvedEffect>(),
      null);

    public static ImmutableState EmptyState => new ImmutableState(
      Player.Player1,
      0,
      false,
      null,
      null,
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToReadOnly(),
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToReadOnly(),
      new List<IActionGroup>().ToImmutableList(),
      new Dictionary<Player, IMutableStackQueue<Card>> {{Player.Player1, new LazyStackQueue<Card>()}, {Player.Player2, new LazyStackQueue<Card>()}}.ToReadOnly()
        .ToImmutable(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToImmutableDictionary()
        .ToImmutable(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToImmutableDictionary()
        .ToImmutable(),
      new Dictionary<Player, IMutableSet<Card>> {{Player.Player1, new LazySet<Card>()}, {Player.Player2, new LazySet<Card>()}}.ToImmutableDictionary()
        .ToImmutable(),
      new Dictionary<Player, IMutableList<Creature>>
        {{Player.Player1, new LazyList<Creature>()}, {Player.Player2, new LazyList<Creature>()}}.ToImmutableDictionary().ToImmutable(),
      ImmutableArray<IEffect>.Empty,
      new List<IResolvedEffect>().ToImmutableList(),
      null);

    public static MutableState New(
      this IState state,
      Player? playerTurn = null,
      int? turnNumber = null,
      bool isGameOver = false,
      KeyforgeUnlocked.Types.Lookup<Player, int> keys = null,
      KeyforgeUnlocked.Types.Lookup<Player, int> aember = null,
      IState previousstate = null,
      House? activeHouse = null,
      IMutableList<IActionGroup> actionGroups = null,
      IReadOnlyDictionary<Player, IMutableStackQueue<Card>> decks = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> hands = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> discards = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> archives = null,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
      IMutableStackQueue<IEffect> effects = null,
      IMutableList<IResolvedEffect> resolvedEffects = null,
      Metadata metadata = null)
    {
      return new MutableState(
        playerTurn ?? state.PlayerTurn,
        turnNumber ?? state.TurnNumber,
        isGameOver || state.IsGameOver,
        previousstate ?? state.PreviousState,
        activeHouse ?? state.ActiveHouse,
        keys ?? new Dictionary<Player, int>(state.Keys).ToLookup(),
        aember ?? new Dictionary<Player, int>(state.Aember).ToLookup(),
        actionGroups ?? new LazyList<IActionGroup>(state.ActionGroups),
        decks ?? state.Decks.ToMutable(),
        hands ?? state.Hands.ToMutable(),
        discards ?? state.Discards.ToMutable(),
        archives ?? state.Archives.ToMutable(),
        fields ?? state.Fields.ToMutable(),
        effects ?? new LazyStackQueue<IEffect>(state.Effects),
        resolvedEffects ?? new LazyList<IResolvedEffect>(state.ResolvedEffects),
        metadata ?? state.Metadata);
    }

    /// <summary>
    /// Returns a new state with "PreviousState" set to the current state, and "ResolvedEffects" cleared
    /// </summary>
    /// <returns></returns>
    public static MutableState Extend(
      this IState state,
      Player? playerTurn = null,
      int? turnNumber = null,
      bool isGameOver = false,
      KeyforgeUnlocked.Types.Lookup<Player, int> keys = null,
      KeyforgeUnlocked.Types.Lookup<Player, int> aember = null,
      House? activeHouse = null,
      IMutableList<IActionGroup> actionGroups = null,
      IReadOnlyDictionary<Player, IMutableStackQueue<Card>> decks = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> hands = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> discards = null,
      IReadOnlyDictionary<Player, IMutableSet<Card>> archives = null,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
      IMutableList<IResolvedEffect> resolvedEffects = null,
      IMutableStackQueue<IEffect> effects = null,
      Metadata metadata = null)
    {
      return state.New(
        playerTurn, turnNumber, isGameOver, keys, aember, state,
        activeHouse, actionGroups, decks, hands, discards, archives,
        fields, effects, resolvedEffects ?? new LazyList<IResolvedEffect>(), metadata);
    }

    static Stack<Card> EmptyDeck => new Stack<Card>();

    public static Stack<Card> SampleDeck =>
      new Stack<Card>(Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()));
  }
}