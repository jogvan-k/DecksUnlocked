using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public class NoActionGroup : ActionGroupBase
  {
    public NoActionGroup() : base(ActionType.NoAction)
    {
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      return ImmutableList<Action>.Empty.Add(new NoAction());
    }
  }
}