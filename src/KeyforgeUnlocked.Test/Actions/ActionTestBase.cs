using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  public abstract class ActionTestBase<T> where T : KeyforgeUnlocked.Actions.Action<T>
  {
    protected void Act(KeyforgeUnlocked.Actions.Action<T> sut,
      MutableState state,
      IState expectedState)
    {
      sut.Validate(state);
      sut.DoActionNoResolve(state);

      StateAsserter.StateEquals(expectedState, state);
    }

    protected void ActExpectException<Texception>(KeyforgeUnlocked.Actions.Action<T> sut,
      MutableState state, System.Action<Texception> callbackAsserts) where Texception : KeyforgeUnlockedException
    {
      try
      {
        sut.Validate(state);
        sut.DoActionNoResolve(state);
      }
      catch (Texception e)
      {
        StateAsserter.StateEquals(state, e.State);
        callbackAsserts(e);
        return;
      }

      Assert.Fail();
    }
  }
}