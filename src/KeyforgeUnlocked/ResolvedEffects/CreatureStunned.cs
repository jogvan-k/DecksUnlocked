using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureStunned : ResolvedEffectWithCreature
  {

    public CreatureStunned(Creature creature) : base(creature)
    {
    }

    public override int GetHashCode()
    {
      return 5 * base.GetHashCode();
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} stunned";
    }
  }
}