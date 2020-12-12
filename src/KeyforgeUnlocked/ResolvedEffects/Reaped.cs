using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class Reaped : ResolvedEffectWithCreature<Reaped>
  {
    public Reaped(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} reaped";
    }
  }
}