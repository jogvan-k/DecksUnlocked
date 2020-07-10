using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class EndTurnTest
  {
    [Test]
    public void DoActionNoResolve_EmptyBoard()
    {
      IState state = TestUtil.EmptyMutableState;
      var sut = new EndTurn();

      state = sut.DoActionNoResolve(state);

      var expectedEffects = new Queue<Effect>();
      expectedEffects.Enqueue(new DrawToHandLimit());
      expectedEffects.Enqueue(new ChangePlayer());
      var expectedState = TestUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}