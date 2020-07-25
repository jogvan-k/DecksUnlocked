using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturePlayed : ResolvedEffectWithCreature
  {
    public readonly int Position;

    public CreaturePlayed(Creature creature,
      int position) : base(creature)
    {
      Position = position;
    }

    bool Equals(CreaturePlayed other)
    {
      return base.Equals(other) && Position == other.Position;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((CreaturePlayed) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Creature, Position);
    }
  }
}