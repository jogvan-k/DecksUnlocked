using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureGainedAember : ResolvedEffectWithCreatureAndInt<CreatureGainedAember>
  {
    public CreatureGainedAember(Creature creature, int @int) : base(creature, @int)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} gained {Int} Aember";
    }
  }
}