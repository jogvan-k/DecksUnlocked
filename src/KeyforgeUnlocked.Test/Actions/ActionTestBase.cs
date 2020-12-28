using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  public abstract class ActionTestBase<T> where T : KeyforgeUnlocked.Actions.Action<T>
  {
    protected void ActAndAssert<T>(T sut,
      MutableState state,
      IState expectedState) where T : KeyforgeUnlocked.Actions.Action<T>
    {
      sut.Validate(state);
      sut.DoActionNoResolve(state);

      StateAsserter.StateEquals(expectedState, state);
    }

    protected void ActExpectException<T, Texception>(T sut,
      MutableState state, System.Action<Texception> callbackAsserts) where Texception : KeyforgeUnlockedException where T : KeyforgeUnlocked.Actions.Action<T>
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