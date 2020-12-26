using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturePlayed : ResolvedEffectWithIdentifiable<CreaturePlayed>
  {
    public readonly int Position;

    public CreaturePlayed(IIdentifiable creature, int position) : base(creature)
    {
      Position = position;
    }

    protected override bool Equals(CreaturePlayed other)
    {
      return base.Equals(other) && Position == other.Position;
    }


    public override string ToString()
    {
      return $"Played {Id.Name} on position {Position}";
    }
  }
}