using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public class EndTurnGroup : ActionGroupBase
  {
    public EndTurnGroup() : base(ActionType.EndTurn)
    {
      Actions = ImmutableList<Action>.Empty.Add(new EndTurn());
    }
  }
}