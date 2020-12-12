using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class KeyForged : Equatable<KeyForged>, IResolvedEffect
  {
    public readonly int KeyCost;

    public KeyForged(int keyCost)
    {
      KeyCost = keyCost;
    }

    protected override bool Equals(KeyForged other)
    {
      return KeyCost == other.KeyCost;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), KeyCost);
    }

    public override string ToString()
    {
      return $"Key forged for {KeyCost} Æmber";
    }
  }
}