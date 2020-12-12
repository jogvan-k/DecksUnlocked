using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureReadied : ResolvedEffectWithCreature
  {
    public CreatureReadied(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} ready";
    }
  }
}