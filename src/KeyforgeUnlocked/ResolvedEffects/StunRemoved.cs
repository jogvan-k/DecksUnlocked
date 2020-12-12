using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class StunRemoved : ResolvedEffectWithCreature<StunRemoved>
  {
    public StunRemoved(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"Stun removed from {Creature.Card.Name}";
    }
  }
}