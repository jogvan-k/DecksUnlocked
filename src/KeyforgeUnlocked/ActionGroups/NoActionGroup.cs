using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public class NoActionGroup : ActionGroupBase
  {
    public NoActionGroup() : base(ActionType.NoAction)
    {
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<Action>.Empty.Add(new NoAction(origin));
    }
  }
}