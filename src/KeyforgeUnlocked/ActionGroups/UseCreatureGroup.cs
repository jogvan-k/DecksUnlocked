using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class UseCreatureGroup : ActionGroupBase<UseCreatureGroup>
  {
    readonly IImmutableList<Creature> _opponentCreatures;
    public readonly Creature Creature;
    readonly bool _allowOutOfHouseUse;
    readonly UseCreature _allowedUsages;

    public UseCreatureGroup(
      IState state,
      Creature creature,
      bool allowOutOfHouseUse = false,
      UseCreature allowedUsages = UseCreature.All)
    {
      _opponentCreatures = state.Fields[state.PlayerTurn.Other()];
      Creature = creature;
      _allowOutOfHouseUse = allowOutOfHouseUse;
      _allowedUsages = allowedUsages;
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      var actions = ImmutableList<IAction>.Empty;
      if (!Creature.IsReady)
        return actions;

      if (Creature.IsStunned())
      {
        if (_allowedUsages == UseCreature.All)
        {
          var action = new RemoveStun(origin, Creature, _allowOutOfHouseUse);
          if (Creature.Card.CardUseActionAllowed(origin, action))
            return actions.Add(action);
        }
        return actions;
      }

      if ((_allowedUsages & UseCreature.Fight) > 0)
      {
        for (int i = 0; i < _opponentCreatures.Count; i++)
        {
          var opponentCreature = _opponentCreatures[i];
          if (opponentCreature.HasTaunt() || !NeighboursHasTaunt(i))
          {
            var action = new FightCreature(origin, Creature, opponentCreature, _allowOutOfHouseUse);
            if(Creature.Card.CardUseActionAllowed(origin, action))
              actions = actions.Add(action);
          }
        }
      }

      if (Creature.Card.CardCreatureAbility != null && (_allowedUsages & UseCreature.ActiveAbility) > 0)
      {
        var action = new UseCreatureAbility(origin, Creature, _allowOutOfHouseUse);
        if(Creature.Card.CardUseActionAllowed(origin, action))
          actions = actions.Add(action);
      }

      if ((_allowedUsages & UseCreature.Reap) > 0)
      {
        var action = new Reap(origin, Creature, _allowOutOfHouseUse);
        if(Creature.Card.CardUseActionAllowed(origin, action))
          actions = actions.Add(action);
      }

      return actions;
    }

    bool NeighboursHasTaunt(int i)
    {
      return ExistsAndHasTaunt(i - 1) || ExistsAndHasTaunt(i + 1);
    }

    bool ExistsAndHasTaunt(int i)
    {
      return i >= 0 && i < _opponentCreatures.Count && _opponentCreatures[i].HasTaunt();
    }

    protected override bool Equals(UseCreatureGroup other)
    {
      return Equals(Creature, other.Creature);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Creature, _allowOutOfHouseUse, _allowedUsages);
    }

    public override string ToString()
    {
      return "Use creature";
    }
  }
}