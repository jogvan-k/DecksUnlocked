using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using DeclareHouse = KeyforgeUnlocked.Effects.DeclareHouse;
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

      var expectedEffects = new StackQueue<IEffect>();
      expectedEffects.Enqueue(new ReadyCards());
      expectedEffects.Enqueue(new DrawToHandLimit());
      expectedEffects.Enqueue(new EndTurn());
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);

      Act(sut, state, expectedState);
    }
  }
}