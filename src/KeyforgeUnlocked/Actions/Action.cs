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
    protected readonly ImmutableState OriginState;
    static readonly Comparer<IAction> Comparer = new ActionStrengthComparer();
    public ICoreState Origin => OriginState;

    protected Action(ImmutableState originState)
    {
      OriginState = originState;
    }

    public ICoreState DoCoreAction()
    {
      return DoAction(OriginState);
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

    internal abstract void DoActionNoResolve(IMutableState state);

    public int CompareTo(object? other)
    {
      if (other == null) return 1;
      return CompareTo((IAction) other);
    }

    int CompareTo(IAction other)
    {
      return Comparer.Compare(this, other);
    }
  }
}