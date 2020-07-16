using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class NoActionGroupTest
  {
    [Test]
    public void Actions()
    {
      var sut = new NoActionGroup();

      var actions = sut.Actions;

      var expectedActions = ImmutableHashSet<Action>.Empty.Add(new NoAction());
      Assert.AreEqual(expectedActions, actions);
    }
  }
}