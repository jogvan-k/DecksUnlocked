using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using UnlockedCore.Actions;
using UnlockedCore.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Actions
{
  public class EndTurn : Action
  {
    public EndTurn(State state) : base(state)
    {
    }

    public override State DoAction()
    {
      var mutableState = State.ToMutable();
      mutableState.Effects.Enqueue(new DrawToHandLimit(mutableState.PlayerTurn));
      mutableState.Effects.Enqueue(new ChangePlayer(mutableState.PlayerTurn));
      return mutableState.ResolveEffects();
    }
  }
}