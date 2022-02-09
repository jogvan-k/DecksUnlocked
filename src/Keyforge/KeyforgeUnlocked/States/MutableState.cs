using System;
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
  public sealed class MutableState : StateBase, IMutableState
  {
    public Player PlayerTurn { get; set; }
    public int TurnNumber { get; set; }
    public bool IsGameOver { get; set; }
    public House? ActiveHouse { get; set; }
    public IMutableLookup<Player, int> Keys { get; set; }
    public IMutableLookup<Player, int> Aember { get; set; }
    public IMutableList<IActionGroup> ActionGroups { get; set; }
    public IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> Decks { get; set; }
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Hands { get; set; }
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Discards { get; set; }
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Archives { get; set; }
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> PurgedCard { get; set; }
    public IReadOnlyDictionary<Player, IMutableList<Creature>> Fields { get; set; }
    public IReadOnlyDictionary<Player, IMutableSet<Artifact>> Artifacts { get; set; }
    public IMutableStackQueue<IEffect> Effects { get; set; }
    public IMutableEvents Events { get; set; }
    public IMutableList<IResolvedEffect> ResolvedEffects { get; set; }
    public IMutableHistoricData HistoricData { get; set; }
    public Metadata Metadata { get; set; }

    #region IState-specific fields
    ImmutableLookup<Player, int> IState.Keys => Keys.Immutable();
    ImmutableLookup<Player, int> IState.Aember => Aember.Immutable();
    IImmutableSet<IActionGroup> IState.ActionGroups => ActionGroups.ToImmutableHashSet();
    IReadOnlyDictionary<Player, IImmutableStack<ICard>> IState.Decks => Decks.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Hands => Hands.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Discards => Discards.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Archives => Archives.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.PurgedCard => PurgedCard.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableList<Creature>> IState.Fields => Fields.ToImmutable();
    IReadOnlyDictionary<Player, IImmutableSet<Artifact>> IState.Artifacts => Artifacts.ToImmutable();
    ImmutableArray<IEffect> IState.Effects => Effects.Immutable();
    ImmutableEvents IState.Events => Events.ToImmutable();
    ImmutableHistoricData IState.HistoricData => HistoricData.ToImmutable();
    IImmutableList<IResolvedEffect> IState.ResolvedEffects => ResolvedEffects.ToImmutableList();
    #endregion

    public MutableState(IState state)
    {
      PlayerTurn = state.PlayerTurn;
      TurnNumber = state.TurnNumber;
      IsGameOver = state.IsGameOver;
      ActiveHouse = state.ActiveHouse;
      Keys = new LazyLookup<Player, int>(state.Keys);
      Aember = new LazyLookup<Player, int>(state.Aember);
      ActionGroups = new LazyList<IActionGroup>(state.ActionGroups);
      Decks = state.Decks.ToMutable();
      Hands = state.Hands.ToMutable();
      Discards = state.Discards.ToMutable();
      Archives = state.Archives.ToMutable();
      PurgedCard = state.PurgedCard.ToMutable();
      Fields = state.Fields.ToMutable();
      Artifacts = state.Artifacts.ToMutable();
      Effects = new LazyStackQueue<IEffect>(state.Effects);
      Events = new LazyEvents(state.Events);
      ResolvedEffects = new LazyList<IResolvedEffect>();
      HistoricData = new LazyHistoricData(state.HistoricData);
      Metadata = state.Metadata;
    }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      House? activeHouse,
      Lookup<Player, int> keys,
      Lookup<Player, int> aember,
      IMutableList<IActionGroup> actionGroups,
      IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> hands,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> discards,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> archives,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> purgedCards,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields,
      IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts,
      IMutableStackQueue<IEffect> effects,
      IMutableEvents events,
      IMutableList<IResolvedEffect> resolvedEffects,
      IMutableHistoricData historicData,
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
      PurgedCard = purgedCards;
      Fields = fields;
      Artifacts = artifacts;
      Effects = effects;
      Events = events;
      ResolvedEffects = resolvedEffects;
      HistoricData = historicData;
      Metadata = metadata;
    }

    void RefreshBaseActions()
    {
      ActionGroups = new LazyList<IActionGroup>();
      if (IsGameOver)
      {
        return;
      }

      ActionGroups.Add(new EndTurnGroup());
      if(!HistoricData.ActionPlayedThisTurn && Archives[PlayerTurn].Count != 0)
        ActionGroups.Add(new TakeArchiveGroup());
      
      var trialState = ToImmutable();
      foreach (var card in Hands[PlayerTurn])
      {
        if (card.House != ActiveHouse)
          continue;
        var actionGroup = ToActionGroup(card);

        if (actionGroup.Actions(trialState).Count != 0)
          ActionGroups.Add(actionGroup);
      }

      foreach (var creature in Fields[PlayerTurn])
      {
        if (creature.IsReady && creature.Card.House == ActiveHouse)
        {
          var actionGroup = new UseCreatureGroup(this, creature);
          if (actionGroup.Actions(trialState).Count != 0)
            ActionGroups.Add(actionGroup);
        }
      }

      foreach (var artifact in Artifacts[PlayerTurn])
      {
        if (artifact.IsReady && artifact.Card.House == ActiveHouse)
        {
          var actionGroup = new UseArtifactGroup(artifact);
          if (actionGroup.Actions(trialState).Count != 0)
            ActionGroups.Add(actionGroup);
        }
      }
    }

    IActionGroup ToActionGroup(ICard card)
    {
      IActionGroup actionGroup;
      switch (card)
      {
        case ICreatureCard creatureCard:
          actionGroup = new PlayCreatureCardGroup(this, creatureCard);
          break;
        case IActionCard actionCard:
          actionGroup = new PlayActionCardGroup(actionCard);
          break;
        case IArtifactCard artifactCard:
          actionGroup = new PlayArtifactCardGroup(artifactCard);
          break;
        default:
          throw new NotImplementedException();
      }

      return actionGroup;
    }

    public ImmutableState ResolveEffects()
    {
      ClearEmptyActionGroups();
      while (ActionGroups.Count == 0 && Effects.Length != 0)
      {
        Effects.Dequeue().Resolve(this);
        ClearEmptyActionGroups();
      }

      if (ActionGroups.Count == 0)
        RefreshBaseActions();

      return ToImmutable();
    }

    void ClearEmptyActionGroups()
    {
      var trialState = ToImmutable();
      foreach (var actionGroup in ActionGroups)
      {
        if (actionGroup.Actions(trialState).Count == 0)
          ActionGroups.Remove(actionGroup);
      }
    }
  }
}