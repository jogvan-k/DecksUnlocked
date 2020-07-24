using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class TargetCreatureGroup : ActionGroupBase
  {
    readonly IImmutableList<Action> actions;

    public TargetCreatureGroup(EffectOnCreature effect, IList<Creature> targets) : base(ActionType.TargetCreature)
    {
      actions = targets.Select(t => (Action) new TargetCreature(effect, t)).ToImmutableList();
    }
    internal TargetCreatureGroup(IEnumerable<Action> actions) : base(ActionType.TargetCreature)
    {
      this.actions = actions.ToImmutableList();
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      return actions;
    }
  }
}