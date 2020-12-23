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
    protected override void ResolveImpl(MutableState state)
    {
      var toDraw = Math.Max(0, EndTurnHandLimit - state.Hands[state.PlayerTurn].Count);
      var cardsDrawn = 0;
      for (var i = 0; i < toDraw; i++)
        if (state.Draw(state.PlayerTurn))
          cardsDrawn++;
      if (cardsDrawn > 0)
        state.ResolvedEffects.Add(new CardsDrawn(state.PlayerTurn, cardsDrawn));
    }
  }
}