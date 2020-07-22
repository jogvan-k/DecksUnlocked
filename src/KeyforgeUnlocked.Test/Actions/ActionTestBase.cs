using System;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlockedTest.Actions
{
  public abstract class ActionTestBase
  {
    protected void Act(Action sut,
      MutableState state,
      IState expectedState)
    {
      sut.Validate(state);
      sut.DoActionNoResolve(state);

      StateAsserter.StateEquals(expectedState, state);
    }

    protected void ActExpectException<T>(Action sut,
      MutableState state,
      Action<T> callbackAsserts) where T : KeyforgeUnlockedException
    {
      try
      {
        Act(sut, state, null);
      }
      catch (T e)
      {
        Assert.AreEqual(state, e.State);
        callbackAsserts(e);
        return;
      }

      Assert.Fail();
    }
  }
}