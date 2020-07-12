using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using EndTurn = KeyforgeUnlocked.Effects.EndTurn;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class EndTurnTest
  {
    [Test]
    public void DoActionNoResolve_EmptyBoard()
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new KeyforgeUnlocked.Actions.EndTurn();

      state = sut.DoActionNoResolve(state);

      var expectedEffects = new Queue<IEffect>();
      expectedEffects.Enqueue(new DrawToHandLimit());
      expectedEffects.Enqueue(new EndTurn());
      var expectedState = TestUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}