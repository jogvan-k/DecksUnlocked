using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;

namespace KeyforgeUnlockedTest.Effects
{
  public sealed class CreatureDied : IResolvedEffect
  {
    public Creature Creature;

    public CreatureDied(Creature creature)
    {
      Creature = creature;
    }

    bool Equals(CreatureDied other)
    {
      return Equals(Creature, other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CreatureDied other && Equals(other);
    }

    public override int GetHashCode()
    {
      return (Creature != null ? Creature.GetHashCode() : 0);
    }
  }
}