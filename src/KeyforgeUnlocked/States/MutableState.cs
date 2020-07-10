using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public class MutableState : StateBase
  {
    public new bool IsGameOver
    {
      get => _isGameOver;
      set => _isGameOver = value;
    }

    public new List<IActionGroup> ActionGroups
    {
      get => _actionGroups;
      set => _actionGroups = value;
    }

    public new Player PlayerTurn
    {
      get => _playerTurn;
      set => _playerTurn = value;
    }

    public new int TurnNumber
    {
      get => _turnNumber;
      set => _turnNumber = value;
    }

    public new Dictionary<Player, Stack<Card>> Decks { get => _decks; set => _decks = value; }

    public new Dictionary<Player, ISet<Card>> Hands { get => _hands; set => _hands = value; }

    public new Dictionary<Player, ISet<Card>> Discards { get => _discards; set => _discards = value; }

    public new Dictionary<Player, ISet<Card>> Archives { get => _archives; set => _discards = value; }

    public new Dictionary<Player, IList<Creature>> Fields { get => _fields; set => _fields = value; }

    public new Queue<Effect> Effects { get => _effects; set => _effects = value; }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      Dictionary<Player, Stack<Card>> decks,
      Dictionary<Player, ISet<Card>> hands,
      Dictionary<Player, ISet<Card>> discards,
      Dictionary<Player, ISet<Card>> archives,
      Dictionary<Player, IList<Creature>> fields,
      Queue<Effect> effects,
      List<IActionGroup> actionGroups)
      : base(
        playerTurn,
        turnNumber,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        actionGroups)
    {
      ActionGroups = new List<IActionGroup>();
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
        if (card.CardType == CardType.Creature)
        {
          var actionGroup = new PlayCreatureCardGroup(this, (CreatureCard) card);
          if (!actionGroup.Actions.IsEmpty)
            ActionGroups.Add(actionGroup);
        }
      }
    }

    public Immutable ResolveEffects()
    {
      while (ActionGroups.Count == 0 && Effects.Count != 0)
        Effects.Dequeue().Resolve(this);

      if (ActionGroups.Count == 0)
        RefreshBaseActions();

      return ToImmutable();
    }
  }
}