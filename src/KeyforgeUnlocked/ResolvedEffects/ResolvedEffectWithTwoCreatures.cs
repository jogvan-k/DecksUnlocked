using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithTwoCreatures<T> : ResolvedEffectWithCreature<T> where T : ResolvedEffectWithTwoCreatures<T>
  {
    public Creature Target;

    public ResolvedEffectWithTwoCreatures(Creature creature, Creature target) : base(creature)
    {
      Target = target;
    }

    protected override bool Equals(T other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Target);
    }
  }
}