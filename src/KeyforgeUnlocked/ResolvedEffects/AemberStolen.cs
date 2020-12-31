using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberStolen : ResolvedEffectWithPlayerAndInt<AemberStolen>
  {
    public AemberStolen(Player stealingPlayer, int stolenAmount) : base(stealingPlayer, stolenAmount)
    {
    }

    public override string ToString()
    {
      return $"{_player} stole {_int} aember";
    }
  }
}