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

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((EndTurn) obj);
    }

    bool Equals(EndTurn other)
    {
      return true;
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}