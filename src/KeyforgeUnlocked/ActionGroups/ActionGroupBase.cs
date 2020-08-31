using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class ActionGroupBase : IActionGroup
  {
    public ActionType Type { get; }

    public IImmutableList<Action> Actions(ImmutableState origin) => InitiateActions(origin);

    public ActionGroupBase(ActionType type)
    {
      Type = type;
    }

    protected abstract IImmutableList<Action> InitiateActions(ImmutableState origin);

    protected bool Equals(ActionGroupBase other)
    {
      return Type.Equals(other.Type);
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
      return Type.GetHashCode();
    }
  }
}