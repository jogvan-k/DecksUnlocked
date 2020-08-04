using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public class UseCreatureGroup : ActionGroupBase
  {
    readonly IList<Creature> _opponentCreatures;
    public Creature Creature;
    readonly bool _allowOutOfHouseUse;

    public UseCreatureGroup(
      IState state,
      Creature creature,
      bool allowOutOfHouseUse = false) : base(ActionType.UseCreature)
    {
      _opponentCreatures = state.Fields[state.PlayerTurn.Other()];
      Creature = creature;
      _allowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      var actions = ImmutableList<Action>.Empty;
      if (!Creature.IsReady)
        return actions;

      if (Creature.IsStunned())
      {
        return actions.Add(new RemoveStun(Creature, _allowOutOfHouseUse));
      }

      for (int i = 0; i < _opponentCreatures.Count; i++)
      {
        var opponentCreature = _opponentCreatures[i];
        if (opponentCreature.HasTaunt() || !NeighboursHasTaunt(i))
          actions = actions.Add(new FightCreature(Creature, opponentCreature, _allowOutOfHouseUse));
      }

      if (Creature.Card.CreatureAbility != null)
      {
        actions = actions.Add(new UseCreatureAbility(Creature, _allowOutOfHouseUse));
      }

      actions = actions.Add(new Reap(Creature, _allowOutOfHouseUse));

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
  }
}