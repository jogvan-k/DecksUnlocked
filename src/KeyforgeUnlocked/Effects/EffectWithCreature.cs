using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.Effects
{
  public abstract class EffectWithCreature<T> : EffectBase<T> where T : EffectWithCreature<T>
  {
    public readonly Creature Creature;

    protected EffectWithCreature(Creature creature)
    {
      Creature = creature;
    }

    protected override bool Equals(T other)
    {
      return Creature.Equals(other.Creature);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Creature);
    }
  }
}