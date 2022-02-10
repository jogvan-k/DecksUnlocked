using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.ActionGroups
{
    sealed class TakeArchiveGroupTest
    {
        [Test]
        public void Actions_NoArchive_NoActions()
        {
            var sut = new TakeArchiveGroup();

            var result = sut.Actions(StateTestUtil.EmptyState);

            Assert.That(result.Count == 0);
        }

        [Test]
        public void Actions_OnlyOpponentHasArchivedCards_NoActions(
            [Values(Player.Player1, Player.Player2)]
            Player player)
        {
            var archives = TestUtil.Sets<ICard>();
            archives[player.Other()].Add(new SampleActionCard());
            var state = StateTestUtil.EmptyState.New(playerTurn: player, archives: archives).ToImmutable();
            var sut = new TakeArchiveGroup();

            var result = sut.Actions(state);

            Assert.That(result.Count == 0);
        }

        [Test]
        public void Actions_playerHasArchivedCards_Actions(
            [Values(Player.Player1, Player.Player2)]
            Player player)
        {
            var archives = TestUtil.Sets<ICard>();
            archives[player].Add(new SampleActionCard());
            var state = StateTestUtil.EmptyState.New(playerTurn: player, archives: archives).ToImmutable();
            var sut = new TakeArchiveGroup();

            var result = sut.Actions(state);

            Assert.That(result.Single(), Is.TypeOf<TakeArchive>());
        }
    }
}