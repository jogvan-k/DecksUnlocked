using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class StunRemoved : IResolvedEffect
  {
    public Creature Creature;

    public StunRemoved(Creature creature)
    {
      Creature = creature;
    }

    bool Equals(StunRemoved other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is StunRemoved other && Equals(other);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}