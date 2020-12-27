using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

[assembly: InternalsVisibleTo("KeyforgeUnlocked.Test")]

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action<T> : Equatable<T>, IAction where T : Action<T>
  {
    protected ImmutableState _origin;
    static Comparer<IAction> _comparer = new ActionStrengthComparer();
    public ICoreState Origin => _origin;

    protected Action(ImmutableState origin)
    {
      _origin = origin;
    }

    public ICoreState DoCoreAction()
    {
      return DoAction(_origin);
    }

    public virtual string Identity()
    {
      return GetType().Name;
    }

    public IState DoAction(IState state)
    {
      Validate(state);
      var mutableState = state.ToMutable();
      mutableState.ActionGroups = new LazyList<IActionGroup>();
      DoActionNoResolve(mutableState);
      return mutableState.ResolveEffects();
    }

    internal virtual void Validate(IState state)
    {
    }

    internal abstract void DoActionNoResolve(MutableState state);

    public int CompareTo(object other)
    {
      return CompareTo((IAction) other);
    }

    int CompareTo(IAction other)
    {
      return _comparer.Compare(this, other);
    }
  }
}