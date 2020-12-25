using System;
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
  public sealed class MutableState : StateBase, IState
  {
    public Player playerTurn;
    public int turnNumber;
    public bool isGameOver;
    public House? activeHouse;
    public Lookup<Player, int> Keys;
    public Lookup<Player, int> Aember;
    public IMutableList<IActionGroup> ActionGroups;
    public IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> Decks;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Hands;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Discards;
    public IReadOnlyDictionary<Player, IMutableSet<ICard>> Archives;
    public IReadOnlyDictionary<Player, IMutableList<Creature>> Fields;
    public IMutableStackQueue<IEffect> Effects;
    public IMutableList<IResolvedEffect> ResolvedEffects;
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

    public new IState PreviousState
    {
      get => previousState;
      set => previousState = value;
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

    ImmutableArray<IEffect> IState.Effects => Effects.Immutable();

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
      PreviousState = state;
      ActiveHouse = state.ActiveHouse;
      Keys = state.Keys.ToLookup();
      Aember = state.Aember.ToLookup();
      ActionGroups = new LazyList<IActionGroup>(state.ActionGroups);
      Decks = state.Decks.ToMutable();
      Hands = state.Hands.ToMutable();
      Discards = state.Discards.ToMutable();
      Archives = state.Archives.ToMutable();
      Fields = state.Fields.ToMutable();
      Effects = new LazyStackQueue<IEffect>(state.Effects);
      ResolvedEffects = new LazyList<IResolvedEffect>();
      Metadata = state.Metadata;
    }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      Lookup<Player, int> keys,
      Lookup<Player, int> aember,
      IMutableList<IActionGroup> actionGroups,
      IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> hands,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> discards,
      IReadOnlyDictionary<Player, IMutableSet<ICard>> archives,
      IReadOnlyDictionary<Player, IMutableList<Creature>> fields,
      IMutableStackQueue<IEffect> effects,
      IMutableList<IResolvedEffect> resolvedEffects,
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

    void RefreshBaseActions()
    {
      ActionGroups = new LazyList<IActionGroup>();
      if (IsGameOver)
      {
        return;
      }

      ActionGroups.Add(new EndTurnGroup());
      
      var trialState = ToImmutable();
      foreach (var card in Hands[PlayerTurn])
      {
        if (card.House != activeHouse)
          continue;
        IActionGroup actionGroup;
        switch (card)
        {
          case CreatureCard creatureCard:
            actionGroup = new PlayCreatureCardGroup(this, creatureCard);
            break;
          case ActionCard actionCard:
            actionGroup = new PlayActionCardGroup(actionCard);
            break;
          default:
            throw new NotImplementedException();
        }

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
    }

    public ImmutableState ResolveEffects()
    {
      while (ActionGroups.Count == 0 && Effects.Length != 0)
        Effects.Dequeue().Resolve(this);

      if (ActionGroups.Count == 0)
        RefreshBaseActions();

      return ToImmutable();
    }
  }
}