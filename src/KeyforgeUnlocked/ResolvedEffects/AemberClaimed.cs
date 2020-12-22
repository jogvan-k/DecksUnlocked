using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberClaimed : ResolvedEffectWithPlayerAndInt<AemberClaimed>
  {

    public AemberClaimed(Player player, int aember) : base(player, aember)
    {
    }

    public override string ToString()
    {
      return $"{_player} claimed {_int} Aember";
    }
  }
}