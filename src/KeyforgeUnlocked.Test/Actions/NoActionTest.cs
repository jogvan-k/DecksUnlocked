using KeyforgeUnlocked.Actions;
using KeyforgeUnlockedTest.Actions;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class NoActionTest : ActionTestBase<NoAction>
  {
    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;

      var expectedState = StateTestUtil.EmptyState;

      Act(new NoAction(null), state, expectedState);
    }
  }
}