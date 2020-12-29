using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
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
    public IReadOnlyDictionary<Player, IImmutableSet<Artifact>> Artifacts { get; }
    public ImmutableArray<IEffect> Effects { get; }
    public IImmutableList<IResolvedEffect> ResolvedEffects { get; }
    public ImmutableHistoricData HistoricData { get; }

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
      IReadOnlyDictionary<Player, IImmutableSet<Artifact>> artifacts,
      ImmutableArray<IEffect> effects,
      IImmutableList<IResolvedEffect> resolvedEffects,
      ImmutableHistoricData historicData,
      Metadata metadata)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      IsGameOver = isGameOver;
      _previousState = previousState;
      ActiveHouse = activeHouse;
      Keys = keys;
      Aember = aember;
      ActionGroups = actionGroups;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Artifacts = artifacts;
      Effects = effects;
      ResolvedEffects = resolvedEffects;
      HistoricData = historicData;
      Metadata = metadata;
    }

    public ImmutableState(IState state)
    {
      PlayerTurn = state.PlayerTurn;
      TurnNumber = state.TurnNumber;
      IsGameOver = state.IsGameOver;
      _previousState = ((StateBase)state)._previousState;
      ActiveHouse = state.ActiveHouse;
      Keys = state.Keys;
      Aember = state.Aember;
      ActionGroups = state.ActionGroups;
      Decks = state.Decks;
      Hands = state.Hands;
      Discards = state.Discards;
      Archives = state.Archives;
      Fields = state.Fields;
      Artifacts = state.Artifacts;
      Effects = state.Effects;
      ResolvedEffects = state.ResolvedEffects;
      HistoricData = state.HistoricData.ToImmutable();
      Metadata = state.Metadata;
    }
  }
}