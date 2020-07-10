using KeyforgeUnlocked.States;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : ICoreAction
  {

    public ICoreState DoCoreAction(ICoreState state)
    {
      return DoAction((IState) state);
    }

    public abstract IState DoAction(IState state);
  }
}