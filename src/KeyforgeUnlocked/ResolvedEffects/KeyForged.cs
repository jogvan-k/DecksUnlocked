using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class KeyForged : ResolvedEffectWithPlayerAndInt<KeyForged>
  {

    public KeyForged(Player player, int keyCost) : base(player, keyCost)
    {
    }

    public override string ToString()
    {
      return $"{_player} forged a key for {_int} Æmber";
    }
  }
}