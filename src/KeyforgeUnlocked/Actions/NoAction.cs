using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class NoAction : Action<NoAction>
  {
    public NoAction(ImmutableState origin) : base(origin)
    {
    }

    internal override void DoActionNoResolve(IMutableState state)
    {
    }

    public override string ToString()
    {
      return "No action";
    }
  }
}