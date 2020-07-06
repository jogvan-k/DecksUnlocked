using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class Action : CoreAction
  {
    public CoreState CoreState => State;

    public State State { get; }
    public Action(State state)
    {
      State = state;
    }

    public CoreState DoCoreAction()
    {
      return DoAction();
    }

    public abstract State DoAction();
  }
}