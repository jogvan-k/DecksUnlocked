using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureFought : IResolvedEffect
  {
    public Creature Fighter;
    public Creature Target;

    public CreatureFought(Creature fighter,
      Creature target)
    {
      Fighter = fighter;
      Target = target;
    }

    bool Equals(CreatureFought other)
    {
      return Equals(Fighter, other.Fighter) && Equals(Target, other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CreatureFought other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Fighter, Target);
    }
  }
}