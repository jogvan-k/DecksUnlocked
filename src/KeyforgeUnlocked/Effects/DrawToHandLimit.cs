using System;
using KeyforgeUnlocked.States;
using UnlockedCore.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  // TODO include shuffle upon empty deck
  public class DrawToHandLimit : Effect
  {
    public DrawToHandLimit(Player player) : base(player)
    {
    }

    public override void Resolve(MutableState state)
    {
      var toDraw = Math.Max(0, EndTurnHandLimit - state.Hands[Player].Count);
      for(var i = 0; i< toDraw; i++)
        state.Draw(Player);
    }
  }
}