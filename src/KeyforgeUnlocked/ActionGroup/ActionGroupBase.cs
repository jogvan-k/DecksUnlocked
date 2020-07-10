using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public abstract class ActionGroupBase : IActionGroup
  {
    public ActionType Type { get; }

    public ImmutableList<Action> Actions { get; protected set; }

    public ActionGroupBase(ActionType type)
    {
      Type = type;
    }
  }
}