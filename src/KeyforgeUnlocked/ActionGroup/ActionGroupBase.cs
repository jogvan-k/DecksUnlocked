using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public abstract class ActionGroupBase : IActionGroup
  {
    public ActionType Type { get; }

    public ImmutableList<Action> Actions { get; }

    public ActionGroupBase(ActionType type,
      IState state)
    {
      Type = type;
      Actions = InitiateActions(state);
    }

    protected abstract ImmutableList<Action> InitiateActions(IState state);
  }
}