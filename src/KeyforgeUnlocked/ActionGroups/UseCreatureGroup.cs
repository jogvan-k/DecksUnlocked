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
    public Creature Creature;
    readonly bool _allowOutOfHouseUse;

    public UseCreatureGroup(
      IState state,
      Creature creature,
      bool allowOutOfHouseUse = false)
    {
      _opponentCreatures = state.Fields[state.PlayerTurn.Other()];
      Creature = creature;
      _allowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      var actions = ImmutableList<IAction>.Empty;
      if (!Creature.IsReady)
        return actions;

      if (Creature.IsStunned())
      {
        return actions.Add(new RemoveStun(origin, Creature, _allowOutOfHouseUse));
      }

      for (int i = 0; i < _opponentCreatures.Count; i++)
      {
        var opponentCreature = _opponentCreatures[i];
        if (opponentCreature.HasTaunt() || !NeighboursHasTaunt(i))
          actions = actions.Add(new FightCreature(origin, Creature, opponentCreature, _allowOutOfHouseUse));
      }

      if (Creature.Card.CardCreatureAbility != null)
      {
        actions = actions.Add(new UseCreatureAbility(origin, Creature, _allowOutOfHouseUse));
      }

      actions = actions.Add(new Reap(origin, Creature, _allowOutOfHouseUse));

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
      return HashCode.Combine(base.GetHashCode(), Creature);
    }
  }
}