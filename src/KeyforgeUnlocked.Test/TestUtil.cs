using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest
{
  public static class TestUtil
  {
    public static MutableState EmptyMutableState => new MutableState(
      Player.Player1,
      0,
      false,
      null,
      new List<IActionGroup>(),
      new Dictionary<Player, Stack<Card>> {{Player.Player1, new Stack<Card>()}, {Player.Player2, new Stack<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, IList<Creature>>
        {{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new Queue<IEffect>(),
      new List<IResolvedEffect>());

    public static ImmutableState EmptyState => new ImmutableState(
      Player.Player1,
      0,
      false,
      null,
      new List<IActionGroup>(),
      new Dictionary<Player, Stack<Card>> {{Player.Player1, new Stack<Card>()}, {Player.Player2, new Stack<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, IList<Creature>>
        {{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new Queue<IEffect>(),
      new List<IResolvedEffect>());

    public static MutableState New(
      this IState state,
      Player? playerTurn = null,
      int? turnNumber = null,
      bool isGameOver = false,
      IState previousState = null,
      IList<IActionGroup> actionGroups = null,
      IDictionary<Player, Stack<Card>> decks = null,
      IDictionary<Player, ISet<Card>> hands = null,
      IDictionary<Player, ISet<Card>> discards = null,
      IDictionary<Player, ISet<Card>> archives = null,
      IDictionary<Player, IList<Creature>> fields = null,
      Queue<IEffect> effects = null,
      IList<IResolvedEffect> resolvedEffects = null)
    {
      return new MutableState(
        playerTurn ?? state.PlayerTurn,
        turnNumber ?? state.TurnNumber,
        isGameOver || state.IsGameOver,
        previousState ?? state.PreviousState,
        actionGroups ?? state.ActionGroups,
        decks ?? state.Decks,
        hands ?? state.Hands,
        discards ?? state.Discards,
        archives ?? state.Archives,
        fields ?? state.Fields,
        effects ?? state.Effects,
        resolvedEffects ?? state.ResolvedEffects);
    }

    public static Stack<Card> SampleDeck =>
      new Stack<Card>(Enumerable.Range(1, 36).Select(i => new SimpleCreatureCard()));
  }
}