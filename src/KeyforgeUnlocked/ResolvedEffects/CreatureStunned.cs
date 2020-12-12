using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureStunned : ResolvedEffectWithCreature<CreatureStunned>
  {

    public CreatureStunned(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} stunned";
    }
  }
}