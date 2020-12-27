using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Actions
{
  public class TakeArchive : Action<TakeArchive>
  {
    public TakeArchive(ImmutableState origin) : base(origin)
    {
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.PopArchive();
    }
  }
}