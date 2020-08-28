using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public sealed class ImmutableState : StateBase, IState
  {
    public Player PlayerTurn { get; }

    public int TurnNumber { get; }

    public bool IsGameOver { get; }

    public IState PreviousState { get; }

    public House? ActiveHouse { get; }

    public IImmutableDictionary<Player, int> Keys { get; }

    public IImmutableDictionary<Player, int> Aember { get; }

    public IImmutableList<IActionGroup> ActionGroups { get; }

    public IImmutableDictionary<Player, IImmutableStack<Card>> Decks { get; }

    public IImmutableDictionary<Player, IImmutableSet<Card>> Hands { get; }

    public IImmutableDictionary<Player, IImmutableSet<Card>> Discards { get; }

    public IImmutableDictionary<Player, IImmutableSet<Card>> Archives { get; }

    public IImmutableDictionary<Player, IImmutableList<Creature>> Fields { get; }

    public ImmutableArray<IEffect> Effects { get; }

    public IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    public Metadata Metadata { get; }

    public ImmutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      IImmutableDictionary<Player, int> keys,
      IImmutableDictionary<Player, int> aember,
      IImmutableList<IActionGroup> actionGroups,
      IImmutableDictionary<Player, IImmutableStack<Card>> decks,
      IImmutableDictionary<Player, IImmutableSet<Card>> hands,
      IImmutableDictionary<Player, IImmutableSet<Card>> discards,
      IImmutableDictionary<Player, IImmutableSet<Card>> archives,
      IImmutableDictionary<Player, IImmutableList<Creature>> fields,
      ImmutableArray<IEffect> effects,
      IImmutableList<IResolvedEffect> resolvedEffects,
      Metadata metadata)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      IsGameOver = isGameOver;
      PreviousState = previousState;
      ActiveHouse = activeHouse;
      Keys = keys;
      Aember = aember;
      ActionGroups = actionGroups;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Effects = effects;
      ResolvedEffects = resolvedEffects;
      Metadata = metadata;
    }

    public ImmutableState(IState state)
    {
      PlayerTurn = state.PlayerTurn;
      TurnNumber = state.TurnNumber;
      IsGameOver = state.IsGameOver;
      PreviousState = state.PreviousState;
      ActiveHouse = state.ActiveHouse;
      Keys = state.Keys;
      Aember = state.Aember;
      ActionGroups = state.ActionGroups;
      Decks = state.Decks;
      Hands = state.Hands;
      Discards = state.Discards;
      Archives = state.Archives;
      Fields = state.Fields;
      Effects = state.Effects;
      ResolvedEffects = state.ResolvedEffects;
      Metadata = state.Metadata;
    }
  }
}