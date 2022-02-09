using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberGained : ResolvedEffectWithPlayerAndInt<AemberGained>
  {
    public AemberGained(Player player, int aember) : base(player, aember)
    {
    }

    public override string ToString()
    {
      return $"{Player} gained {_int} aember";
    }
  }
}