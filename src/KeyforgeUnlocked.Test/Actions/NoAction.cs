using KeyforgeUnlockedTest.Actions;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class NoAction : ActionTestBase
  {
    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;

      var expectedState = StateTestUtil.EmptyState;

      Act(new KeyforgeUnlocked.Actions.NoAction(null), state, expectedState);
    }
  }
}