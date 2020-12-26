using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureDied : ResolvedEffectWithIdentifiable<CreatureDied>
  {

    public CreatureDied(IIdentifiable creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} died";
    }
  }
}