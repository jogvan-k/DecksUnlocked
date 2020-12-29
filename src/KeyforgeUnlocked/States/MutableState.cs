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
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public sealed class MutableState : StateBase, IState
  {
    public Player playerTurn;
    public int turnNumber;
    public bool isGameOver;
    public House? activeHouse;
    public Types.Lookup<Player, int> Keys;
    public Types.Lookup<Player, int> Aember;
    public IMutableList<IActionGroup> ActionGroups;
    public IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> Decks;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Hands;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Discards;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Archives;
    public IReadOnlyDictionary<Player, IMutableList<Creature>> Fields;
    public IReadOnlyDictionary<Player, IMutableSet<Artifact>> Artifacts;
    public IMutableStackQueue<IEffect> Effects;
    public IMutableList<IResolvedEffect> ResolvedEffects;
    public IMutableHistoricData HistoricData;
    public Metadata metadata;

    public Player PlayerTurn
    {
      get => playerTurn;
      set => playerTurn = value;
    }

    public int TurnNumber
    {
      get => turnNumber;
      set => turnNumber = value;
    }

    public bool IsGameOver
    {
      get => isGameOver;
      set => isGameOver = value;
    }

    public House? ActiveHouse
    {
      get => activeHouse;
      set => activeHouse = value;
    }

    IReadOnlyDictionary<Player, int> IState.Keys => Keys.ToReadOnly();

    IReadOnlyDictionary<Player, int> IState.Aember => Aember.ToReadOnly();

    IImmutableSet<IActionGroup> IState.ActionGroups => ActionGroups.ToImmutableHashSet();

    IReadOnlyDictionary<Player, IImmutableStack<ICard>> IState.Decks => Decks.ToImmutable();

    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Hands => Hands.ToImmutable();

    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Discards => Discards.ToImmutable();

    IReadOnlyDictionary<Player, IImmutableSet<ICard>> IState.Archives => Archives.ToImmutable();

    IReadOnlyDictionary<Player, IImmutableList<Creature>> IState.Fields => Fields.ToImmutable();

    IReadOnlyDictionary<Player, IImmutableSet<Artifact>> IState.Artifacts => Artifacts.ToImmutable();

    ImmutableArray<IEffect> IState.Effects => Effects.Immutable();

    ImmutableHistoricData IState.HistoricData => HistoricData.ToImmutable();

    IImmutableList<IResolvedEffect> IState.ResolvedEffects => ResolvedEffects.ToImmutableList();


    public Metadata Metadata
    {
      get => metadata;
      set => metadata = value;
    }

    public MutableState(IState state)
    {
      PlayerTurn = state.PlayerTurn;
      TurnNumber = state.TurnNumber;
      IsGameOver = state.IsGameOver;
      ActiveHouse = state.ActiveHouse;
      Keys = state.Keys.ToLookup();
      Aember = state.Aember.ToLookup();
      ActionGroups = new LazyList<IActionGroup>(state.ActionGroups);
      Decks = state.Decks.ToMutable();
      Hands = state.Hands.ToMutable();
      Discards = state.Discards.ToMutable();
      Archives = state.Archives.ToMutable();
      Fields = state.Fields.ToMutable();
      Artifacts = state.Artifacts.ToMutable();
      Effects = new LazyStackQueue<IEffect>(state.Effects);
      ResolvedEffects = new LazyList<IResolvedEffect>();
      HistoricData = new LazyHistoricData(state.HistoricData);
      Metadata = state.Metadata;
    }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      House? activeHouse, Types.Lookup<Player, int> keys, Types.Lookup<Player, int> aember,
      IMutableList<IActionGroup> actionGroups,
      IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> hands,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> discards,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> archives,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields,
      IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts,
      IMutableStackQueue<IEffect> effects,
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
      Fields = fields;
      Artifacts = artifacts;
      Effects = effects;
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
        if (card.House != activeHouse)
          continue;
        var actionGroup = ToActionGroup(card);

        if (actionGroup.Actions(trialState).Count != 0)
          ActionGroups.Add(actionGroup);
      }

      foreach (var creature in Fields[PlayerTurn])
      {
        if (creature.IsReady && creature.Card.House == activeHouse)
        {
          var actionGroup = new UseCreatureGroup(this, creature);
          if (actionGroup.Actions(trialState).Count != 0)
            ActionGroups.Add(actionGroup);
        }
      }

      foreach (var artifact in Artifacts[playerTurn])
      {
        if (artifact.IsReady && artifact.Card.House == activeHouse)
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