using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public class NoActionGroup : ActionGroupBase
  {
    public NoActionGroup() : base(ActionType.NoAction)
    {
    }

    protected override IImmutableSet<Action> InitiateActions()
    {
      return ImmutableHashSet<Action>.Empty.Add(new NoAction());
    }
  }
}