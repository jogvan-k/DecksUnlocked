using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class StunRemoved : ResolvedEffectWithCreature
  {
    public StunRemoved(Creature creature) : base(creature)
    {
    }

    public override int GetHashCode()
    {
      return 2 * base.GetHashCode();
    }
  }
}