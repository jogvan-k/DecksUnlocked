using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Actions
{
  public class TakeArchive : Action<TakeArchive>
  {
    public TakeArchive(ImmutableState originState) : base(originState)
    {
    }

    internal override void DoActionNoResolve(IMutableState state)
    {
      state.PopArchive();
    }
  }
}