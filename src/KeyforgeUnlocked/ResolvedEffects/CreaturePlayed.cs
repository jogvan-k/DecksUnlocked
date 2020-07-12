using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturePlayed : IResolvedEffect
  {
    public Creature Creature { get; }

    public int Position { get; }

    public CreaturePlayed(Creature creature,
      int position)
    {
      Creature = creature;
      Position = position;
    }

    bool Equals(CreaturePlayed other)
    {
      return Equals(Creature, other.Creature) && Position == other.Position;
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