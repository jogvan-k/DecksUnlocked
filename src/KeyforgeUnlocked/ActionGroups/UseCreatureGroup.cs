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

    public UseCreatureGroup(
      IState state,
      Creature creature) : base(ActionType.UseCreature)
    {
      _opponentCreatures = state.Fields[state.PlayerTurn.Other()];
      Creature = creature;
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      var actions = ImmutableList<Action>.Empty;
      if (!Creature.IsReady)
        return actions;

      if (Creature.IsStunned())
      {
        return actions.Add(new RemoveStun(Creature));
      }

      for (int i = 0; i < _opponentCreatures.Count; i++)
      {
        var opponentCreature = _opponentCreatures[i];
        if (opponentCreature.HasTaunt() || !NeighboursHasTaunt(i))
          actions = actions.Add(new FightCreature(Creature, opponentCreature));
      }

      if (Creature.Card.CreatureAbility != null)
      {
        actions = actions.Add(new UseCreatureAbility(Creature));
      }

      actions = actions.Add(new Reap(Creature));

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