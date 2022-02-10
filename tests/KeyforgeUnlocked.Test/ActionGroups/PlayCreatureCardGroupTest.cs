using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.ActionGroups
{
    [TestFixture]
    class PlayCreatureCardGroupTest
    {
        static readonly ICreatureCard Card = new SampleCreatureCard();
        private ImmutableState _state = StateTestUtil.EmptyState;

        [Test]
        public void Actions_EmptyState()
        {
            var sut = new PlayCreatureCardGroup(_state, Card);

            var actions = sut.Actions(_state);

            var expectedAction =
                ImmutableList<IAction>.Empty.Add(new PlayCreatureCard(_state, Card, 0))
                    .Add(new DiscardCard(_state, Card));
            Assert.AreEqual(expectedAction, actions);
        }

        [Test]
        public void Actions_CreaturesOnBoard_ActionsOnlyOnFlank()
        {
            var mutableState = InitiateStateWithCreatures();
            var sut = new PlayCreatureCardGroup(mutableState, Card);

            var immutableState = mutableState.ToImmutable();
            var actions = sut.Actions(immutableState);

            var expectedActions =
                ImmutableList<IAction>.Empty
                    .Add(new PlayCreatureCard(immutableState, Card, 0))
                    .Add(new PlayCreatureCard(immutableState, Card, 5))
                    .Add(new DiscardCard(immutableState, Card));
            Assert.AreEqual(expectedActions, actions);
        }

        [Test]
        public void Actions_PlayNotAllowed()
        {
            var card = new SampleCreatureCard(playCardAllowed: (_, _) => false);
            var mutableState = InitiateStateWithCreatures();
            var sut = new PlayCreatureCardGroup(mutableState, card);

            var immutableState = mutableState.ToImmutable();
            var actions = sut.Actions(immutableState);

            var expectedActions =
                ImmutableList<IAction>.Empty
                    .Add(new DiscardCard(immutableState, card));
            Assert.AreEqual(expectedActions, actions);
        }

        IMutableState InitiateStateWithCreatures()
        {
            var mutableState = _state.ToMutable();
            for (int i = 0; i < 5; i++)
                mutableState.Fields[Player.Player1].Add(new Creature(new SampleCreatureCard()));
            return mutableState;
        }
    }
}