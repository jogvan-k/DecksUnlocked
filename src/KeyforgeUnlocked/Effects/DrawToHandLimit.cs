using System;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using UnlockedCore.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  // TODO include shuffle upon empty deck
  public sealed class DrawToHandLimit : IEffect
  {
    public void Resolve(MutableState state)
    {
      var toDraw = Math.Max(0, EndTurnHandLimit - state.Hands[state.PlayerTurn].Count);
      var cardsDrawn = 0;
      for (var i = 0; i < toDraw; i++)
        if (state.Draw(state.PlayerTurn))
          cardsDrawn++;
      if (cardsDrawn > 0)
        state.ResolvedEffects.Add(new CardsDrawn(cardsDrawn));
    }

    bool Equals(DrawToHandLimit other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DrawToHandLimit) obj);
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}