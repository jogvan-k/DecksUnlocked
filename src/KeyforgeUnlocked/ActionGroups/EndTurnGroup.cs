using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class EndTurnGroup : ActionGroupBase
  {
    public EndTurnGroup() : base(ActionType.EndTurn)
    {
      Actions = ImmutableList<Action>.Empty.Add(new EndTurn());
    }
  }
}