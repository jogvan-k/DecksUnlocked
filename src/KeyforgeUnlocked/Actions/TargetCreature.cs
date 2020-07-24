using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Actions
{
  public sealed class TargetCreature : Action
  {
    readonly EffectOnCreature _effect;
    public readonly Creature Target;

    public TargetCreature(EffectOnCreature effect, Creature target)
    {
      _effect = effect;
      Target = target;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      _effect(state, Target);
    }

    bool Equals(TargetCreature other)
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