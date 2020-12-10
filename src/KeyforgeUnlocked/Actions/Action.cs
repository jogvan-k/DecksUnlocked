using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

[assembly: InternalsVisibleTo("KeyforgeUnlocked.Test")]

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : ICoreAction
  {
    protected ImmutableState _origin;
    static Comparer<Action> _comparer = new ActionStrengthComparer();
    public ICoreState Origin => _origin;

    protected Action(ImmutableState origin)
    {
      _origin = origin;
    }

    public ICoreState DoCoreAction()
    {
      return DoAction(_origin);
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

    /// <summary>
    /// Gives a string-based identity of the action. Used to compare actions to ensure the same ordering. The identity
    /// should therefore be different from action's identity of the same type for a given <see cref="Origin"/>.
    /// </summary>
    /// <returns></returns>
    public virtual string Identity()
    {
      return "";
    }

    public int CompareTo(object other)
    {
      return CompareTo((Action) other);
    }

    int CompareTo(Action other)
    {
      return _comparer.Compare(this, other);
    }
  }
}