using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public sealed class MutableState : StateBase, IState
  {
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

    public IDictionary<Player, int> Keys
    {
      get => keys;
      set => keys = value;
    }

    public IDictionary<Player, int> Aember
    {
      get => aember;
      set => aember = value;
    }

    public IList<IResolvedEffect> ResolvedEffects
    {
      get => resolvedEffects;
      set => resolvedEffects = value;
    }

    public IList<IActionGroup> ActionGroups
    {
      get => actionGroups;
      set => actionGroups = value;
    }

    public IDictionary<Player, Stack<Card>> Decks
    {
      get => decks;
      set => decks = value;
    }

    public IDictionary<Player, ISet<Card>> Hands
    {
      get => hands;
      set => hands = value;
    }

    public IDictionary<Player, ISet<Card>> Discards
    {
      get => discards;
      set => discards = value;
    }

    public IDictionary<Player, ISet<Card>> Archives
    {
      get => archives;
      set => discards = value;
    }

    public IDictionary<Player, IList<Creature>> Fields
    {
      get => fields;
      set => fields = value;
    }

    public StackQueue<IEffect> Effects
    {
      get => effects;
      set => effects = value;
    }

    public Metadata Metadata
    {
      get => metadata;
      set => metadata = value;
    }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOVer,
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
      : base(
        playerTurn,
        turnNumber,
        isGameOVer,
        previousState,
        activeHouse,
        keys,
        aember,
        actionGroups,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        resolvedEffects,
        metadata)
    {
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
        if (card.CardType == CardType.Creature && card.House == activeHouse)
        {
          var actionGroup = new PlayCreatureCardGroup(this, (CreatureCard) card);
          if (actionGroup.Actions.Count != 0)
            ActionGroups.Add(actionGroup);
        }
      }

      foreach (var creature in Fields[PlayerTurn])
      {
        if (creature.IsReady && creature.Card.House == activeHouse)
        {
          var actionGroup = new UseCreatureGroup(creature);
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