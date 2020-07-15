using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ActionGroups
{
  public class UseCreatureGroup : ActionGroupBase
  {
    public Creature Creature;

    public UseCreatureGroup(Creature creature) : base(ActionType.UseCreature)
    {
      Creature = creature;
      Actions = InitiateActions();
    }

    ImmutableList<Action> InitiateActions()
    {
      var actions = ImmutableList<Action>.Empty;
      if (Creature.IsReady)
      {
        actions = actions.Add(new Reap(Creature.Id));
      }

      return actions;
    }
  }
}