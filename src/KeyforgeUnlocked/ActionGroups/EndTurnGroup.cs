using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class EndTurnGroup : ActionGroupBase
  {

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<Action>.Empty.Add(new EndTurn(origin));
    }
  }
}