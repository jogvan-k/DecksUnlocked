using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureCardPlayed : ResolvedEffectWithIdentifiable<CreatureCardPlayed>
  {
    public readonly int Position;

    public CreatureCardPlayed(IIdentifiable creature, int position) : base(creature)
    {
      Position = position;
    }

    protected override bool Equals(CreatureCardPlayed other)
    {
      return base.Equals(other) && Position == other.Position;
    }


    public override string ToString()
    {
      return $"Played {Id.Name} on position {Position}";
    }
  }
}