using System;
using UnlockedCore.States;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberLost :  IResolvedEffect
  {
    public Player Player;
    public int Aember;

    public AemberLost(Player player, int aember)
    {
      Player = player;
      Aember = aember;
    }

    bool Equals(AemberLost other)
    {
      return Player == other.Player && Aember == other.Aember;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is AemberLost other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 3 * HashCode.Combine((int) Player, Aember);
    }

    public override string ToString()
    {
      return $"{Player} lost {Aember} Æmber";
    }
  }
}