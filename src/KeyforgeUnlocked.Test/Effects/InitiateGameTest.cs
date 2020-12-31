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
        new LazyStackQueue<IEffect>(new[] {(IEffect) new EndTurn(), new DrawToHandLimit(), new ReadyCardsAndRestoreArmor(), new FirstTurn(), new DeclareHouse(), new DrawInitialHands()});
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}