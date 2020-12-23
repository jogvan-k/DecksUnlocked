using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class CreatureDamaged : ResolvedEffectWithCreatureAndInt<CreatureDamaged>
  {
    public CreatureDamaged(Creature creature, int @int) : base(creature, @int)
    {
    }

    public override string ToString()
    {
      return $"{Int} damage dealt to {Creature.Card.Name}";
    }
  }
}