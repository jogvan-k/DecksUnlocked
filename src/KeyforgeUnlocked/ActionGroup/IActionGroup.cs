using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroup
{
  public interface IActionGroup
  {
    public ActionType Type { get; }

    public ImmutableList<Action> Actions { get; }
  }
}