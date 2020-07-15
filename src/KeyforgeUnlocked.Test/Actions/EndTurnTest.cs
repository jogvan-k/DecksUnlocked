using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using EndTurn = KeyforgeUnlocked.Effects.EndTurn;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class EndTurnTest : ActionTestBase
  {
    [Test]
    public void Act_EmptyBoard()
    {
      var state = StateUtil.EmptyMutableState;
      var sut = new KeyforgeUnlocked.Actions.EndTurn();

      Act(sut, state);

      var expectedEffects = new Queue<IEffect>();
      expectedEffects.Enqueue(new ReadyCards());
      expectedEffects.Enqueue(new DrawToHandLimit());
      expectedEffects.Enqueue(new EndTurn());
      var expectedState = StateUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}