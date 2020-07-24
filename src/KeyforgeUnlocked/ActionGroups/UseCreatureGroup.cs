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
      foreach (var targetCreature in _opponentCreatures)
      {
        actions = actions.Add(new FightCreature(Creature, targetCreature));
      }

      if (Creature.Card.CreatureAbility != null)
      {
        actions = actions.Add(new UseCreatureAbility(Creature));
      }

      actions = actions.Add(new Reap(Creature));

      return actions;
    }
  }
}