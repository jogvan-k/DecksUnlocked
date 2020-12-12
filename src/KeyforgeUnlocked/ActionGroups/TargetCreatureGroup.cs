using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class TargetCreatureGroup : ResolveEffectActionGroup
  {
    readonly EffectOnCreature _effect;
    readonly IMutableList<Creature> _targets;

    public TargetCreatureGroup(EffectOnCreature effect, IMutableList<Creature> targets)
    {
      _effect = effect;
      _targets = targets;
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return _targets.Select(t => (Action) new TargetCreature(origin, _effect, t)).ToImmutableList();
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _effect.GetHashCode());
    }
  }
}