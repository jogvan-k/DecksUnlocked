using System;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardsDrawn : ResolvedEffectWithPlayerAndInt<CardsDrawn>
  {

    public CardsDrawn(Player player, int noDrawn) : base(player, noDrawn)
    {
      if (noDrawn <= 0)
        throw new ArgumentOutOfRangeException($"Argument 'noDrawn' was {noDrawn}, must at least be 1.");
    }

    public override string ToString()
    {
      return $"{_player} drew {_int} cards";
    }
  }
}