using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class StunRemoved : ResolvedEffectWithIdentifiable<StunRemoved>
  {
    public StunRemoved(IIdentifiable creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"Stun removed from {Id.Name}";
    }
  }
}