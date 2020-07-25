using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureFought : ResolvedEffectWithCreature
  {
    public readonly Creature Target;

    public CreatureFought(Creature fighter,
      Creature target) : base(fighter)
    {
      Target = target;
    }

    bool Equals(CreatureFought other)
    {
      return base.Equals(other) && Equals(Target, other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CreatureFought other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Creature, Target);
    }
  }
}