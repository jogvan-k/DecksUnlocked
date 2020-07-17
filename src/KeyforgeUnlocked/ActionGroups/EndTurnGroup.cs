using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class EndTurnGroup : ActionGroupBase
  {
    public EndTurnGroup() : base(ActionType.EndTurn)
    {
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      return ImmutableList<Action>.Empty.Add(new EndTurn());
    }
  }
}