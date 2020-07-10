using KeyforgeUnlocked.States;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : CoreAction
  {

    public CoreState DoCoreAction(CoreState state)
    {
      return DoAction((IState) state);
    }

    public abstract IState DoAction(IState state);
  }
}