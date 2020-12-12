using System;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class KeyForged : IResolvedEffect
  {
    public readonly int KeyCost;

    public KeyForged(int keyCost)
    {
      KeyCost = keyCost;
    }

    bool Equals(KeyForged other)
    {
      return KeyCost == other.KeyCost;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((KeyForged) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(GetType(), KeyCost);
    }

    public override string ToString()
    {
      return $"Key forged for {KeyCost} Æmber";
    }
  }
}