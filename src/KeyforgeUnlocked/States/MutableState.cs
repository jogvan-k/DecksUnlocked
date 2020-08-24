using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.Cards.CreatureCards;
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
    public IState previousState;
    public House? activeHouse;
    public IDictionary<Player, int> Keys;
    public IDictionary<Player, int> Aember;
    public IList<IActionGroup> ActionGroups;
    public IDictionary<Player, Stack<Card>> Decks;
    public IDictionary<Player, ISet<Card>> Hands;
    public IDictionary<Player, ISet<Card>> Discards;
    public IDictionary<Player, ISet<Card>> Archives;
    public IDictionary<Player, IList<Creature>> Fields;
    public StackQueue<IEffect> Effects;
    public IList<IResolvedEffect> ResolvedEffects;
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

    public IState PreviousState
    {
      get => previousState;
      set => previousState = value;
    }

    public House? ActiveHouse
    {
      get => activeHouse;
      set => activeHouse = value;
    }

    IImmutableDictionary<Player, int> IState.Keys => Keys.ToImmutableDictionary();

    IImmutableDictionary<Player, int> IState.Aember => Aember.ToImmutableDictionary();

    IImmutableList<IActionGroup> IState.ActionGroups => ActionGroups.ToImmutableList();

    IImmutableDictionary<Player, Stack<Card>> IState.Decks => Decks.ToImmutableDictionary();

    IImmutableDictionary<Player, ISet<Card>> IState.Hands => Hands.ToImmutableDictionary();

    IImmutableDictionary<Player, ISet<Card>> IState.Discards => Discards.ToImmutableDictionary();

    IImmutableDictionary<Player, ISet<Card>> IState.Archives => Archives.ToImmutableDictionary();

    IImmutableDictionary<Player, IList<Creature>> IState.Fields => Fields.ToImmutableDictionary();

    ImmutableArray<IEffect> IState.Effects => Effects.ToImmutableArray();

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
      Keys = new Dictionary<Player, int>(state.Keys);
      Aember = new Dictionary<Player, int>(state.Aember);
      ActionGroups = new List<IActionGroup>(state.ActionGroups);
      Decks = new Dictionary<Player, Stack<Card>>(state.Decks);
      Hands = new Dictionary<Player, ISet<Card>>(state.Hands);
      Discards = new Dictionary<Player, ISet<Card>>(state.Discards);
      Archives = new Dictionary<Player, ISet<Card>>(state.Archives);
      Fields = new Dictionary<Player, IList<Creature>>(state.Fields);
      Effects = new StackQueue<IEffect>(state.Effects);
      ResolvedEffects = new List<IResolvedEffect>();
      Metadata = state.Metadata;
    }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      IDictionary<Player, int> keys,
      IDictionary<Player, int> aember,
      IList<IActionGroup> actionGroups,
      IDictionary<Player, Stack<Card>> decks,
      IDictionary<Player, ISet<Card>> hands,
      IDictionary<Player, ISet<Card>> discards,
      IDictionary<Player, ISet<Card>> archives,
      IDictionary<Player, IList<Creature>> fields,
      StackQueue<IEffect> effects,
      IList<IResolvedEffect> resolvedEffects,
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
      ActionGroups = new List<IActionGroup>();
      if (IsGameOver)
      {
        return;
      }

      ActionGroups.Add(new EndTurnGroup());
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

        if (actionGroup.Actions.Count != 0)
          ActionGroups.Add(actionGroup);
      }

      foreach (var creature in Fields[PlayerTurn])
      {
        if (creature.IsReady && creature.Card.House == activeHouse)
        {
          var actionGroup = new UseCreatureGroup(this, creature);
          if (actionGroup.Actions.Count != 0)
            ActionGroups.Add(actionGroup);
        }
      }
    }

    public ImmutableState ResolveEffects()
    {
      while (ActionGroups.Count == 0 && Effects.Count != 0)
        Effects.Dequeue().Resolve(this);

      if (ActionGroups.Count == 0)
        RefreshBaseActions();

      return ToImmutable();
    }
  }
}