using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithCreature<T> : Equatable<T>, IResolvedEffect where T : ResolvedEffectWithCreature<T>
  {
    public Creature Creature;

    protected ResolvedEffectWithCreature(Creature creature)
    {
      Creature = creature;
    }

    protected override bool Equals(T other)
    {
      return Creature.Equals(other.Creature);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Creature.GetHashCode());
    }
  }
}