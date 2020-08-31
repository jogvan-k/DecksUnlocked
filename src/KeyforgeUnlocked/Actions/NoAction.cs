using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class NoAction : Action
  {
    public NoAction(ImmutableState originState) : base(originState)
    {
    }

    internal override void DoActionNoResolve(MutableState state)
    {
    }

    bool Equals(NoAction other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((NoAction) obj);
    }

    public override int GetHashCode()
    {
      return typeof(NoAction).GetHashCode();
    }
  }
}