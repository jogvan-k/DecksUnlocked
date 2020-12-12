using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Actions
{
  public sealed class TargetCreature : Action<TargetCreature>
  {
    readonly EffectOnCreature _effect;
    public readonly Creature Target;

    public TargetCreature(ImmutableState origin, EffectOnCreature effect, Creature target) : base(origin)
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
      _origin.FindCreature(Target.Id, out var player, out var index);
      return player.ToString() + index;
    }

    protected override bool Equals(TargetCreature other)
    {
      return Equals(_effect, other._effect) && Target.Equals(other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is TargetCreature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(_effect, Target);
    }
  }
}