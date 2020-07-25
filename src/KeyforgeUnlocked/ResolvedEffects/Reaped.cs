using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class Reaped : ResolvedEffectWithCreature
  {
    public Reaped(Creature creature) : base(creature)
    {
    }

    public override int GetHashCode()
    {
      return 3 * base.GetHashCode();
    }
  }
}