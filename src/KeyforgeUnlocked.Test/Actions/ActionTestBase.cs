using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedTest.Actions
{
  public abstract class ActionTestBase
  {
    protected void Act(Action sut, MutableState state)
    {
      sut.Validate(state);
      sut.DoActionNoResolve(state);
    }
  }
}