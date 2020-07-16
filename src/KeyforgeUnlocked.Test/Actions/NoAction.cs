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

      Act(new KeyforgeUnlocked.Actions.NoAction(), state);

      var expectedState = StateTestUtil.EmptyState;
      Assert.AreEqual(expectedState, state);
    }
  }
}