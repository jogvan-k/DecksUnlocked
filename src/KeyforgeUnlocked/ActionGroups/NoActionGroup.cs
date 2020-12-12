using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class NoActionGroup : ActionGroupBase<NoActionGroup>
  {
    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<IAction>.Empty.Add(new NoAction(origin));
    }
  }
}