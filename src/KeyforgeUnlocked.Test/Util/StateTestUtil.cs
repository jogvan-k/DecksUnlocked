using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

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
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}},
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}},
      new List<IActionGroup>(),
      new Dictionary<Player, Stack<Card>> {{Player.Player1, new Stack<Card>()}, {Player.Player2, new Stack<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, IList<Creature>>
        {{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new StackQueue<IEffect>(),
      new List<IResolvedEffect>(),
      null);

    public static ImmutableState EmptyState => new ImmutableState(
      Player.Player1,
      0,
      false,
      null,
      null,
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToImmutableDictionary(),
      new Dictionary<Player, int> {{Player.Player1, 0}, {Player.Player2, 0}}.ToImmutableDictionary(),
      new List<IActionGroup>().ToImmutableList(),
      new Dictionary<Player, Stack<Card>> {{Player.Player1, new Stack<Card>()}, {Player.Player2, new Stack<Card>()}}.ToImmutableDictionary(),
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}}.ToImmutableDictionary(),
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}}.ToImmutableDictionary(),
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}}.ToImmutableDictionary(),
      new Dictionary<Player, IList<Creature>>
        {{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}}.ToImmutableDictionary(),
      ImmutableArray<IEffect>.Empty,
      new List<IResolvedEffect>().ToImmutableList(),
      null);

    public static MutableState New(
      this IState state,
      Player? playerTurn = null,
      int? turnNumber = null,
      bool isGameOver = false,
      IDictionary<Player, int> keys = null,
      IDictionary<Player, int> aember = null,
      IState previousstate = null,
      House? activeHouse = null,
      IList<IActionGroup> actionGroups = null,
      IDictionary<Player, Stack<Card>> decks = null,
      IDictionary<Player, ISet<Card>> hands = null,
      IDictionary<Player, ISet<Card>> discards = null,
      IDictionary<Player, ISet<Card>> archives = null,
      IDictionary<Player, IList<Creature>> fields = null,
      StackQueue<IEffect> effects = null,
      IList<IResolvedEffect> resolvedEffects = null,
      Metadata metadata = null)
    {
      return new MutableState(
        playerTurn ?? state.PlayerTurn,
        turnNumber ?? state.TurnNumber,
        isGameOver || state.IsGameOver,
        previousstate ?? state.PreviousState,
        activeHouse ?? state.ActiveHouse,
        keys ?? new Dictionary<Player, int>(state.Keys),
        aember ?? new Dictionary<Player, int>(state.Aember),
        actionGroups ?? new List<IActionGroup>(state.ActionGroups),
        decks ?? new Dictionary<Player, Stack<Card>>(state.Decks),
        hands ?? new Dictionary<Player, ISet<Card>>(state.Hands),
        discards ?? new Dictionary<Player, ISet<Card>>(state.Discards),
        archives ?? new Dictionary<Player, ISet<Card>>(state.Archives),
        fields ?? new Dictionary<Player, IList<Creature>>(state.Fields),
        effects ?? new StackQueue<IEffect>(state.Effects),
        resolvedEffects ?? new List<IResolvedEffect>(state.ResolvedEffects),
        metadata ?? state.Metadata);
    }

    static Stack<Card> EmptyDeck => new Stack<Card>();

    public static Stack<Card> SampleDeck =>
      new Stack<Card>(Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()));
  }
}