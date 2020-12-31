using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  public abstract class ActionTestBase<T> where T : Action<T>
  {
    protected void ActAndAssert(T sut,
      IMutableState state,
      IState expectedState)
    {
      sut.Validate(state);
      sut.DoActionNoResolve(state);

      StateAsserter.StateEquals(expectedState, state);
    }

    protected void ActExpectException<Texception>(T sut,
      IMutableState state, System.Action<Texception> callbackAsserts) where Texception : KeyforgeUnlockedException
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