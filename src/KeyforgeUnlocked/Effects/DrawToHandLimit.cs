using System;
using KeyforgeUnlocked.States;
using UnlockedCore.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  // TODO include shuffle upon empty deck
  public class DrawToHandLimit : Effect
  {
    public override void Resolve(MutableState state)
    {
      var toDraw = Math.Max(0, EndTurnHandLimit - state.Hands[state.PlayerTurn].Count);
      for(var i = 0; i< toDraw; i++)
        state.Draw(state.PlayerTurn);
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