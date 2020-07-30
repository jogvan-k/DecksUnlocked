using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithCreature : IResolvedEffect
  {
    public Creature Creature;

    protected ResolvedEffectWithCreature(Creature creature)
    {
      Creature = creature;
    }

    protected virtual bool Equals(ResolvedEffectWithCreature other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ResolvedEffectWithCreature) obj);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}