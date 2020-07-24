using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureStunned : IResolvedEffect
  {
    public Creature Creature;

    public CreatureStunned(Creature creature)
    {
      Creature = creature;
    }

    bool Equals(CreatureStunned other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CreatureStunned other && Equals(other);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}