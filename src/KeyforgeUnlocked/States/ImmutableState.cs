using System.Collections.Generic;
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
    public House? ActiveHouse { get; }
    public IReadOnlyDictionary<Player, int> Keys { get; }
    public IReadOnlyDictionary<Player, int> Aember { get; }
    public IImmutableSet<IActionGroup> ActionGroups { get; }
    public IReadOnlyDictionary<Player, IImmutableStack<ICard>> Decks { get; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Hands { get; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Discards { get; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Archives { get; }
    public IReadOnlyDictionary<Player, IImmutableList<Creature>> Fields { get; }
    public ImmutableArray<IEffect> Effects { get; }
    public IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    public Metadata Metadata { get; }

    public ImmutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      LookupReadOnly<Player, int> keys,
      LookupReadOnly<Player, int> aember,
      IImmutableSet<IActionGroup> actionGroups,
      LookupReadOnly<Player, IImmutableStack<ICard>> decks,
      LookupReadOnly<Player, IImmutableSet<ICard>> hands,
      LookupReadOnly<Player, IImmutableSet<ICard>> discards,
      LookupReadOnly<Player, IImmutableSet<ICard>> archives,
      LookupReadOnly<Player, IImmutableList<Creature>> fields,
      ImmutableArray<IEffect> effects,
      IImmutableList<IResolvedEffect> resolvedEffects,
      Metadata metadata)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      IsGameOver = isGameOver;
      this.previousState = previousState;
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
      previousState = ((StateBase)state).previousState;
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