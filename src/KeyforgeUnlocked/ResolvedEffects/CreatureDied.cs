using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureDied : ResolvedEffectWithCreature
  {

    public CreatureDied(Creature creature) : base(creature)
    {
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
      return 7 * Creature.GetHashCode();
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} died";
    }
  }
}