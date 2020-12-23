using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithCreatureAndInt<T> : ResolvedEffectWithCreature<T> where T : ResolvedEffectWithCreatureAndInt<T>
  {
    public readonly int Int;

    protected ResolvedEffectWithCreatureAndInt(Creature creature, int @int) : base(creature)
    {
      Int = @int;
    }

    protected override bool Equals(T other)
    {
      return base.Equals(other) && Int == other.Int;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Int) ;
    }
  }
}