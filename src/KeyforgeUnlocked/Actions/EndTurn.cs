using KeyforgeUnlocked.States;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Actions
{
  public class EndTurn : Action
  {
    const int HandLimit = 6;
    public EndTurn(State state) : base(state)
    {
    }

    public override State DoAction()
    {
      var mutableState = State.ToMutable();
      var toDraw = HandLimit - State.Hands[State.PlayerTurn].Count;
      if (toDraw > 0)
      {
        //mutableState.Draw(mutableState.PlayerTurn, toDraw);
      }

      mutableState.PlayerTurn = mutableState.PlayerTurn.Other();
      mutableState.TurnNumber++;
      return mutableState.ResolveEffects();
    }
  }
}