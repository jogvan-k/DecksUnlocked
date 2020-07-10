using System.Runtime.CompilerServices;
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
      return DoActionNoResolve(state).ResolveEffects();
    }

    internal abstract MutableState DoActionNoResolve(IState state);
  }
}