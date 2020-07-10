using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public class EndTurnGroup : ActionGroupBase
  {
    public EndTurnGroup(IState state) : base(ActionType.EndTurn, state)
    {
    }

    protected override ImmutableList<Action> InitiateActions(IState state)
    {
      return ImmutableList<Action>.Empty.Add(new EndTurn());
    }
  }
}