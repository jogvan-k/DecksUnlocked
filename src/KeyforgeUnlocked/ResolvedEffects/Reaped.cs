using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class Reaped : IResolvedEffect
  {
    public Creature Creature;

    public Reaped(Creature creature)
    {
      Creature = creature;
    }

    bool Equals(Reaped other)
    {
      return Equals(Creature, other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is Reaped other && Equals(other);
    }

    public override int GetHashCode()
    {
      return (Creature != null ? Creature.GetHashCode() : 0);
    }
  }
}