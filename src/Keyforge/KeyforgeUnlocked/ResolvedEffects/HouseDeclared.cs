using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class HouseDeclared : Equatable<HouseDeclared>, IResolvedEffect
  {
    public readonly House House;

    public HouseDeclared(House house)
    {
      House = house;
    }

    protected override bool Equals(HouseDeclared other)
    {
      return House == other.House;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), House);
    }

    public override string ToString()
    {
      return $"House {House} declared.";
    }
  }
}