using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using UnlockedCore;

[assembly: InternalsVisibleTo("KeyforgeUnlocked.Test")]

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : ICoreAction
  {
    private ImmutableState _originState;

    public ICoreState Origin => _originState;

    protected Action(ImmutableState originState)
    {
      _originState = originState;
    }

    public ICoreState DoCoreAction()
    {
      return DoAction(_originState);
    }

    public IState DoAction(IState state)
    {
      Validate(state);
      var mutableState = state.ToMutable();
      mutableState.ActionGroups = new List<IActionGroup>();
      DoActionNoResolve(mutableState);
      return mutableState.ResolveEffects();
    }

    internal virtual void Validate(IState state)
    {
    }

    internal abstract void DoActionNoResolve(MutableState state);
  }
}