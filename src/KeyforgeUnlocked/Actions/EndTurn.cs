using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class EndTurn : BasicAction
  {
    internal override MutableState DoActionNoResolve(MutableState state)
    {
      Validate(state);
      state.Effects.Enqueue(new DrawToHandLimit());
      state.Effects.Enqueue(new Effects.EndTurn());
      return state;
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