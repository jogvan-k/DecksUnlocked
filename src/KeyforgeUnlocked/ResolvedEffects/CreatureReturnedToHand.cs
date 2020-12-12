using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureReturnedToHand : ResolvedEffectWithCreature
  {
    public CreatureReturnedToHand(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} returned to hand";
    }
  }
}