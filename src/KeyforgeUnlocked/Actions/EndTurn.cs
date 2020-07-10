using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class EndTurn : Action
  {
    public override IState DoAction(IState state)
    {
      var mutableState = state.ToMutable();
      mutableState.Effects.Enqueue(new DrawToHandLimit(mutableState.PlayerTurn));
      mutableState.Effects.Enqueue(new ChangePlayer(mutableState.PlayerTurn));
      return mutableState.ResolveEffects();
    }
  }
}