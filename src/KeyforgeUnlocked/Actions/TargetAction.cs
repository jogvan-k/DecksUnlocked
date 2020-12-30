using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Actions
{
  public sealed class TargetAction : Action<TargetAction>
  {
    readonly Callback _effect;
    public readonly IIdentifiable Target;
    readonly Player _owningPayer;

    public TargetAction(ImmutableState origin, Callback effect, IIdentifiable target, Player owningPayer) : base(origin)
    {
      _effect = effect;
      Target = target;
      _owningPayer = owningPayer;
    }

    internal override void DoActionNoResolve(IMutableState state)
    {
      _effect(state, Target, _owningPayer);
    }

    public override string Identity()
    {
      return Target.Id;
    }

    protected override bool Equals(TargetAction other)
    {
      return Equals(_effect, other._effect) && Target.Equals(other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is TargetAction other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(_effect, Target);
    }
  }
}