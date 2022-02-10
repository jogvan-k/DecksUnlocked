using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
    [TestFixture]
    public class EndTurnGroupTest
    {
        private ImmutableState _state = StateTestUtil.EmptyState;

        [Test]
        public void Actions()
        {
            var sut = new EndTurnGroup();

            var actions = sut.Actions(_state);

            Assert.AreEqual(new EndTurn(_state), actions.Single());
        }
    }
}