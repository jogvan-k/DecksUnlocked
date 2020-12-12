using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class NoActionGroupTest
  {
    private ImmutableState _state = StateTestUtil.EmptyState;

    [Test]
    public void Actions()
    {
      var sut = new NoActionGroup();

      var actions = sut.Actions(_state);

      var expectedActions = ImmutableHashSet<IAction>.Empty.Add(new NoAction(_state));
      Assert.AreEqual(expectedActions, actions);
    }
  }
}