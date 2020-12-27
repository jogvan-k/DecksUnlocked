using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public class TakeArchiveGroup : ActionGroupBase<TakeArchiveGroup>
  {
    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      if(origin.Archives[origin.PlayerTurn].Count == 0)
        return ImmutableList<IAction>.Empty;

      return new[] { (IAction) new TakeArchive(origin) }.ToImmutableList();
    }
  }
}