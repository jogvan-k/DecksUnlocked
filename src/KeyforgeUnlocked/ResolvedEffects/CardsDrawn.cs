using System;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardsDrawn : IResolvedEffect
  {
    public readonly int NoDrawn;

    public CardsDrawn(int noDrawn)
    {
      if (noDrawn <= 0)
        throw new ArgumentOutOfRangeException($"Argument 'noDrawn' was {noDrawn}, must at least be 1.");
      NoDrawn = noDrawn;
    }

    bool Equals(CardsDrawn other)
    {
      return NoDrawn == other.NoDrawn;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((CardsDrawn) obj);
    }

    public override int GetHashCode()
    {
      return NoDrawn;
    }
  }
}