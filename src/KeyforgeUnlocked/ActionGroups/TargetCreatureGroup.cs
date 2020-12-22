using System;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class TargetCreatureGroup : ResolveEffectActionGroup<TargetCreatureGroup>
  {
    readonly EffectOnCreature _effect;
    readonly IMutableList<Creature> _targets;

    public TargetCreatureGroup(EffectOnCreature effect, IMutableList<Creature> targets)
    {
      _effect = effect;
      _targets = targets;
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return _targets.Select(t => (IAction) new TargetCreature(origin, _effect, t)).ToImmutableList();
    }

    protected override bool Equals(TargetCreatureGroup other)
    {
      return Equals(_effect, other._effect) && Equals(_targets, other._targets);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _effect, _targets);
    }
  }
}