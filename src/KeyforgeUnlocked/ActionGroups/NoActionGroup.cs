using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public class NoActionGroup : ActionGroupBase
  {
    public NoActionGroup() : base(ActionType.NoAction)
    {
      Actions = ImmutableHashSet<Action>.Empty.Add(new NoAction());
    }
  }
}