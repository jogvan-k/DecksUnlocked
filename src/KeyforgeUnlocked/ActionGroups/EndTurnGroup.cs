using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class EndTurnGroup : ActionGroupBase
  {
    public EndTurnGroup() : base(ActionType.EndTurn)
    {
    }

    protected override IImmutableSet<Action> InitiateActions()
    {
      return ImmutableHashSet<Action>.Empty.Add(new EndTurn());
    }
  }
}