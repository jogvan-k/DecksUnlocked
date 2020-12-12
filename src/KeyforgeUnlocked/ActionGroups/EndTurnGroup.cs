using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class EndTurnGroup : ActionGroupBase<EndTurnGroup>
  {
    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<IAction>.Empty.Add(new EndTurn(origin));
    }
  }
}