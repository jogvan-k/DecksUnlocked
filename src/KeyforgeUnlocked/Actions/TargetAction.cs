using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Actions
{
  public sealed class TargetAction : Action<TargetAction>
  {
    readonly EffectOnTarget _effect;
    public readonly IIdentifiable Target;

    public TargetAction(ImmutableState origin, EffectOnTarget effect, IIdentifiable target) : base(origin)
    {
      _effect = effect;
      Target = target;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      _effect(state, Target);
    }

    public override string Identity()
    {
      _origin.FindCreature(Target, out var player, out var index);
      return player.ToString() + index;
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