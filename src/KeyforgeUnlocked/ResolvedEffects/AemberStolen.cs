using System;
using UnlockedCore.States;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberStolen : IResolvedEffect
  {
    public readonly Player StealingPlayer;
    public readonly int StolenAmount;

    public AemberStolen(Player stealingPlayer, int stolenAmount)
    {
      if (stolenAmount < 0)
        throw new ArgumentOutOfRangeException(nameof(stolenAmount));
      StealingPlayer = stealingPlayer;
      StolenAmount = stolenAmount;
    }

    bool Equals(AemberStolen other)
    {
      return StolenAmount == other.StolenAmount && StealingPlayer == other.StealingPlayer;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is AemberStolen other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 2 * HashCode.Combine(StolenAmount, (int) StealingPlayer);
    }

    public override string ToString()
    {
      return $"{StealingPlayer} stole {StolenAmount} aember";
    }
  }
}