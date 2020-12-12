using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberStolen : Equatable<AemberStolen>, IResolvedEffect
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

    protected override bool Equals(AemberStolen other)
    {
      return StolenAmount == other.StolenAmount && StealingPlayer == other.StealingPlayer;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), StolenAmount, StealingPlayer);
    }

    public override string ToString()
    {
      return $"{StealingPlayer} stole {StolenAmount} aember";
    }
  }
}