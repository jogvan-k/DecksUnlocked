using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class ActionGroupBase : IActionGroup
  {
    public ActionType Type { get; }

    public IImmutableSet<Action> Actions { get; protected set; }

    public ActionGroupBase(ActionType type)
    {
      Type = type;
    }

    protected bool Equals(ActionGroupBase other)
    {
      return Actions.SequenceEqual(other.Actions);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ActionGroupBase) obj);
    }

    public override int GetHashCode()
    {
      return (Actions != null ? Actions.GetHashCode() : 0);
    }
  }
}