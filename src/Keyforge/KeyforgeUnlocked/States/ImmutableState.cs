using System.Collections.Generic;
using System.Collections.Immutable;
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
  public sealed class ImmutableState : StateBase, IState
  {
    public Player PlayerTurn { get; init; }
    public int TurnNumber { get; init; }
    public bool IsGameOver { get; init; }
    public House? ActiveHouse { get; init; }
    public ImmutableLookup<Player, int> Keys { get; init; }
    public ImmutableLookup<Player, int> Aember { get; init; }
    public IImmutableSet<IActionGroup> ActionGroups { get; init; }
    public IReadOnlyDictionary<Player, IImmutableStack<ICard>> Decks { get; init; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Hands { get; init; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Discards { get; init; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> Archives { get; init; }
    public IReadOnlyDictionary<Player, IImmutableSet<ICard>> PurgedCard { get; init; }
    public IReadOnlyDictionary<Player, IImmutableList<Creature>> Fields { get; init; }
    public IReadOnlyDictionary<Player, IImmutableSet<Artifact>> Artifacts { get; init; }
    public ImmutableArray<IEffect> Effects { get; init; }
    public ImmutableEvents Events { get; init; }
    public IImmutableList<IResolvedEffect> ResolvedEffects { get; init; }
    public ImmutableHistoricData HistoricData { get; init; }
    public Metadata Metadata { get; init; }

    public ImmutableState()
    {
    }

    public ImmutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      House? activeHouse,
      ImmutableLookup<Player, int> keys,
      ImmutableLookup<Player, int> aember,
      IImmutableSet<IActionGroup> actionGroups,
      ImmutableLookup<Player, IImmutableStack<ICard>> decks,
      ImmutableLookup<Player, IImmutableSet<ICard>> hands,
      ImmutableLookup<Player, IImmutableSet<ICard>> discards,
      ImmutableLookup<Player, IImmutableSet<ICard>> archives,
      IReadOnlyDictionary<Player, IImmutableSet<ICard>> purgedCard,
      ImmutableLookup<Player, IImmutableList<Creature>> fields,
      IReadOnlyDictionary<Player, IImmutableSet<Artifact>> artifacts,
      ImmutableArray<IEffect> effects,
      ImmutableEvents events,
      IImmutableList<IResolvedEffect> resolvedEffects,
      ImmutableHistoricData historicData,
      Metadata metadata)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      IsGameOver = isGameOver;
      ActiveHouse = activeHouse;
      Keys = keys;
      Aember = aember;
      ActionGroups = actionGroups;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      PurgedCard = purgedCard;
      Fields = fields;
      Artifacts = artifacts;
      Effects = effects;
      Events = events;
      ResolvedEffects = resolvedEffects;
      HistoricData = historicData;
      Metadata = metadata;
    }

    public ImmutableState(IState state)
    {
      PlayerTurn = state.PlayerTurn;
      TurnNumber = state.TurnNumber;
      IsGameOver = state.IsGameOver;
      ActiveHouse = state.ActiveHouse;
      Keys = state.Keys;
      Aember = state.Aember;
      ActionGroups = state.ActionGroups;
      Decks = state.Decks;
      Hands = state.Hands;
      Discards = state.Discards;
      Archives = state.Archives;
      PurgedCard = state.PurgedCard;
      Fields = state.Fields;
      Artifacts = state.Artifacts;
      Effects = state.Effects;
      Events = state.Events;
      ResolvedEffects = state.ResolvedEffects;
      HistoricData = state.HistoricData.ToImmutable();
      Metadata = state.Metadata;
    }
  }
}