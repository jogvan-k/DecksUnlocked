using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithTwoCreatures : ResolvedEffectWithCreature
  {
    public Creature Target;

    public ResolvedEffectWithTwoCreatures(Creature creature, Creature target) : base(creature)
    {
      Target = target;
    }

    protected bool Equals(ResolvedEffectWithTwoCreatures other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ResolvedEffectWithTwoCreatures) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Target);
    }
  }
}