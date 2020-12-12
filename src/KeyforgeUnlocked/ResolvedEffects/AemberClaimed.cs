using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberClaimed : Equatable<AemberClaimed>, IResolvedEffect
  {
    public readonly Player Player;
    public readonly int Aember;

    public AemberClaimed(Player player, int aember)
    {
      Player = player;
      Aember = aember;
    }

    protected override bool Equals(AemberClaimed other)
    {
      return Player == other.Player && Aember == other.Aember;
    }


    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Player, Aember);
    }

    public override string ToString()
    {
      return $"{Player} claimed {Aember} Aember";
    }
  }
}