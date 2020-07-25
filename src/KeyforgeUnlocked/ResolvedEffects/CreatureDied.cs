using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureDied : ResolvedEffectWithCreature
  {

    public CreatureDied(Creature creature) : base(creature)
    {
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