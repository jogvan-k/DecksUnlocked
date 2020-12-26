using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureReturnedToHand : ResolvedEffectWithIdentifiable<CreatureReturnedToHand>
  {
    public CreatureReturnedToHand(IIdentifiable creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} returned to hand";
    }
  }
}