using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public interface IActionGroup
  {
    public ActionType Type { get; }

    public IImmutableList<Action> Actions { get; }
  }
}