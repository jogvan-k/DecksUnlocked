using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class ActionGroupBase : IActionGroup
  {
    readonly System.Lazy<IImmutableSet<Action>> _actions;
    public ActionType Type { get; }

    public IImmutableSet<Action> Actions => _actions.Value;

    public ActionGroupBase(ActionType type)
    {
      Type = type;
      _actions = new System.Lazy<IImmutableSet<Action>>(InitiateActions);
    }

    protected abstract IImmutableSet<Action> InitiateActions();

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