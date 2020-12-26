using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureGainedAember : ResolvedEffectWithIdentifiableAndInt<CreatureGainedAember>
  {
    public CreatureGainedAember(IIdentifiable creature, int @int) : base(creature, @int)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} gained {Int} Aember";
    }
  }
}