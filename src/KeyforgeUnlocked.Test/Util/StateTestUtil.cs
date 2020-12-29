using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
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
      new Dictionary<Player, IMutableStackQueue<ICard>> {{Player.Player1, new LazyStackQueue<ICard>()}, {Player.Player2, new LazyStackQueue<ICard>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<ICard>> {{Player.Player1, new LazySet<ICard>()}, {Player.Player2, new LazySet<ICard>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<ICard>> {{Player.Player1, new LazySet<ICard>()}, {Player.Player2, new LazySet<ICard>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableSet<ICard>> {{Player.Player1, new LazySet<ICard>()}, {Player.Player2, new LazySet<ICard>()}}.ToReadOnly(),
      new Dictionary<Player, IMutableList<Creature>>
        {{Player.Player1, new LazyList<Creature>()}, {Player.Player2, new LazyList<Creature>()}}.ToReadOnly(),
      TestUtil.Sets<Artifact>(),
      new LazyStackQueue<IEffect>(),
      new LazyList<IResolvedEffect>(),
      new LazyHistoricData(),
      null);

    public static ImmutableState EmptyState => new ImmutableState(
      Player.Player1,
      0,
      false,
      null,
      null,
      TestUtil.Ints().ToReadOnly(),
      TestUtil.Ints().ToReadOnly(),
      new HashSet<IActionGroup>().ToImmutableHashSet(),
      TestUtil.Stacks<ICard>().ToImmutable()
      ,
      TestUtil.Sets<ICard>().ToImmutable(),
      TestUtil.Sets<ICard>().ToImmutable(),
      TestUtil.Sets<ICard>().ToImmutable(),
      TestUtil.Lists<Creature>().ToImmutable(),
      TestUtil.Sets<Artifact>().ToImmutable(),
      ImmutableArray<IEffect>.Empty,
      new List<IResolvedEffect>().ToImmutableList(),
      new ImmutableHistoricData(),
      null);

    public static MutableState New(
      this IState state,
      Player? playerTurn = null,
      int? turnNumber = null,
      bool isGameOver = false,
      KeyforgeUnlocked.Types.Lookup<Player, int> keys = null,
      KeyforgeUnlocked.Types.Lookup<Player, int> aember = null,
      IState previousState = null,
      House? activeHouse = null,
      IMutableList<IActionGroup> actionGroups = null,
      IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> hands = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> discards = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> archives = null,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
      IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts = null,
      IMutableStackQueue<IEffect> effects = null,
      IMutableList<IResolvedEffect> resolvedEffects = null,
      IMutableHistoricData historicData = null,
      Metadata metadata = null)
    {
      return new MutableState(
        playerTurn ?? state.PlayerTurn,
        turnNumber ?? state.TurnNumber,
        isGameOver || state.IsGameOver,
        previousState ?? (IState) state.PreviousState?.Value,
        activeHouse ?? state.ActiveHouse,
        keys ?? new Dictionary<Player, int>(state.Keys).ToLookup(),
        aember ?? new Dictionary<Player, int>(state.Aember).ToLookup(),
        actionGroups ?? new LazyList<IActionGroup>(state.ActionGroups),
        decks ?? state.Decks.ToMutable(),
        hands ?? state.Hands.ToMutable(),
        discards ?? state.Discards.ToMutable(),
        archives ?? state.Archives.ToMutable(),
        fields ?? state.Fields.ToMutable(),
        artifacts ?? state.Artifacts.ToMutable(),
        effects ?? new LazyStackQueue<IEffect>(state.Effects),
        resolvedEffects ?? new LazyList<IResolvedEffect>(state.ResolvedEffects),
        historicData ?? new LazyHistoricData(),
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
      IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> hands = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> discards = null,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> archives = null,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
      IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts = null,
      IMutableList<IResolvedEffect> resolvedEffects = null,
      IMutableHistoricData historicData = null,
      IMutableStackQueue<IEffect> effects = null,
      Metadata metadata = null)
    {
      return state.New(
        playerTurn, turnNumber, isGameOver, keys, aember, state,
        activeHouse, actionGroups, decks, hands, discards, archives,
        fields, artifacts, effects, resolvedEffects ?? new LazyList<IResolvedEffect>(), historicData, metadata);
    }

    static Stack<ICard> EmptyDeck => new();

    public static Stack<ICard> SampleDeck =>
      new(Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()));
  }
}