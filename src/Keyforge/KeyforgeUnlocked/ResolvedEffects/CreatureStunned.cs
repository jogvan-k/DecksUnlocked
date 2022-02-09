using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureStunned : ResolvedEffectWithIdentifiable<CreatureStunned>
  {

    public CreatureStunned(IIdentifiable creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} stunned";
    }
  }
}