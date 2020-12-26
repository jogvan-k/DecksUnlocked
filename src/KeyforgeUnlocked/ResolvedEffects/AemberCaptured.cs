using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberCaptured : ResolvedEffectWithIdentifiableAndInt<AemberCaptured>
  {
    public AemberCaptured(IIdentifiable creature, int captured) : base(creature, captured)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} captured {Int} Aember";
    }
  }
}