using System;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  // TODO include shuffle upon empty deck
  public sealed class DrawToHandLimit : EffectBase<DrawToHandLimit>
  {
    protected override void ResolveImpl(IMutableState state)
    {
      var toDraw = Math.Max(0, EndTurnHandLimit - state.Hands[state.PlayerTurn].Count);
      state.Draw(state.PlayerTurn, toDraw);
    }
  }
}