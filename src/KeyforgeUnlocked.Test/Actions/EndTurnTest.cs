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
      var state = StateTestUtil.EmptyMutableState;
      var sut = new KeyforgeUnlocked.Actions.EndTurn();

      Act(sut, state);

      var expectedEffects = new Queue<IEffect>();
      expectedEffects.Enqueue(new ReadyCards());
      expectedEffects.Enqueue(new DrawToHandLimit());
      expectedEffects.Enqueue(new EndTurn());
      expectedEffects.Enqueue(new TryForge());
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}