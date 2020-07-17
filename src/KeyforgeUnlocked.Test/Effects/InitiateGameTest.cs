using System.Collections.Generic;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class InitiateGameTest
  {
    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new InitiateGame();

      sut.Resolve(state);

      var expectedEffects =
        new StackQueue<IEffect>(new[] {(IEffect) new EndTurn(), new FirstTurn(), new DeclareHouse(), new DrawInitialHands()});
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}