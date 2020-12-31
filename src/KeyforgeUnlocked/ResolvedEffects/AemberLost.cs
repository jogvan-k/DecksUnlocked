using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberLost : ResolvedEffectWithPlayerAndInt<AemberLost>
  {

    public AemberLost(Player player, int aember) : base(player, aember)
    {
    }

    public override string ToString()
    {
      return $"{_player} lost {_int} Æmber";
    }
  }
}