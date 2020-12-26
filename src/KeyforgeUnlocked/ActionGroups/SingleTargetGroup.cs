using System;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class SingleTargetGroup : ResolveEffectActionGroup<SingleTargetGroup>
  {
    readonly EffectOnTarget _effect;
    readonly IImmutableList<IIdentifiable> _targets;

    public SingleTargetGroup(EffectOnTarget effect, IImmutableList<IIdentifiable> targets)
    {
      _effect = effect;
      _targets = targets;
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return _targets.Select(t => (IAction) new TargetAction(origin, _effect, t)).ToImmutableList();
    }

    protected override bool Equals(SingleTargetGroup other)
    {
      return Equals(_effect, other._effect) && _targets.SequenceEqual(other._targets);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _effect, EqualityComparer.GetHashCode(_targets));
    }
  }
}