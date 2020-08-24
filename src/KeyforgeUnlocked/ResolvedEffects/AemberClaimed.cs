using System;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberClaimed : IResolvedEffect
  {
    public readonly Player Player;
    public readonly int Aember;

    public AemberClaimed(Player player, int aember)
    {
      Player = player;
      Aember = aember;
    }

    bool Equals(AemberClaimed other)
    {
      return Player == other.Player && Aember == other.Aember;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is AemberClaimed other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine((int) Player, Aember);
    }

    public override string ToString()
    {
      return $"{Player} claimed {Aember} Aember";
    }
  }
}