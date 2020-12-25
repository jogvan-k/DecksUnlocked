using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class CreatureHealed : ResolvedEffectWithCreatureAndInt<CreatureHealed>
  {
    public CreatureHealed(Creature creature, int @int) : base(creature, @int)
    {
    }
    
    public override string ToString()
    {
      return $"{Int} damage healed from {Creature.Card.Name}";
    }
  }
}