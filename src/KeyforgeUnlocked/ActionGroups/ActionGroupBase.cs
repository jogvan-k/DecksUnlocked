using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
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