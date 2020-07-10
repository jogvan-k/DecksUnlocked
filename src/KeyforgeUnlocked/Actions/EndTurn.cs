using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class EndTurn : BasicAction
  {
    internal override MutableState DoActionNoResolve(IState state)
    {
      Validate(state);
      var mutableState = state.ToMutable();
      mutableState.Effects.Enqueue(new DrawToHandLimit());
      mutableState.Effects.Enqueue(new ChangePlayer());
      return mutableState;
    }
  }
}