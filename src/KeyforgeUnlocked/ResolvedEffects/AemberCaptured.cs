using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberCaptured : ResolvedEffectWithCreature<AemberCaptured>
  {
    public readonly int Captured;
    public AemberCaptured(Creature creature, int captured) : base(creature)
    {
      Captured = captured;
    }

    protected override bool Equals(AemberCaptured other)
    {
      return base.Equals(other) && Captured == other.Captured;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Captured);
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} captured {Captured} Aember";
    }
  }
}