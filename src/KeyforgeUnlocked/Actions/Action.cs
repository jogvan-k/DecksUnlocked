using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using UnlockedCore.Actions;
using UnlockedCore.States;

[assembly: InternalsVisibleTo("KeyforgeUnlocked.Test")]

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : ICoreAction
  {
    public ICoreState DoCoreAction(ICoreState state)
    {
      return DoAction((IState) state);
    }

    public IState DoAction(IState state)
    {
      Validate(state);
      var mutableState = state.ToMutable();
      mutableState.ActionGroups = new List<IActionGroup>();
      DoActionNoResolve(mutableState);
      return mutableState.ResolveEffects();
    }

    internal abstract void Validate(IState state);

    internal abstract void DoActionNoResolve(MutableState state);
  }
}