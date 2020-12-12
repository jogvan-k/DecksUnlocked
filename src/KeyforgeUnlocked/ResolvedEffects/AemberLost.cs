using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberLost : Equatable<AemberLost>, IResolvedEffect
  {
    public Player Player;
    public int Aember;

    public AemberLost(Player player, int aember)
    {
      Player = player;
      Aember = aember;
    }

    protected override bool Equals(AemberLost other)
    {
      return Player == other.Player && Aember == other.Aember;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Player, Aember);
    }

    public override string ToString()
    {
      return $"{Player} lost {Aember} Æmber";
    }
  }
}