using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types.Events;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  // TODO include shuffle upon empty deck
  public sealed class DrawToHandLimit : EffectBase<DrawToHandLimit>
  {
    protected override void ResolveImpl(IMutableState state)
    {
      var endTurnHandLimit = EndTurnHandLimit + state.Modify(ModifierType.HandLimit);
      var toDraw = Math.Max(0, endTurnHandLimit - state.Hands[state.PlayerTurn].Count);
      state.Draw(state.PlayerTurn, toDraw);
    }
  }
}