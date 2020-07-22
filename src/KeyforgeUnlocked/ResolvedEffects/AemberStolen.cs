using System;
using UnlockedCore.States;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberStolen : IResolvedEffect
  {
    public Player stealingPlayer;
    public int stolenAmount;

    public AemberStolen(Player stealingPlayer, int stolenAmount)
    {
      if (stolenAmount < 0)
        throw new ArgumentOutOfRangeException(nameof(stolenAmount));
      this.stealingPlayer = stealingPlayer;
      this.stolenAmount = stolenAmount;
    }

    bool Equals(AemberStolen other)
    {
      return stolenAmount == other.stolenAmount && stealingPlayer == other.stealingPlayer;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is AemberStolen other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(stolenAmount, (int) stealingPlayer);
    }
  }
}