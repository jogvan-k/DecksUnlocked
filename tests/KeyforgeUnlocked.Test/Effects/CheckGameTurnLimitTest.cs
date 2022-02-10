using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
    [TestFixture]
    sealed class CheckGameTurnLimitTest
    {
        static readonly Metadata Metadata = new Metadata(Initializers.EmptyDeck(), Initializers.EmptyHouses(), 10, 0);

        [Test]
        public void GameTurnLimitNotReached()
        {
            var initialState = StateTestUtil.EmptyMutableState;
            initialState.TurnNumber = 5;
            initialState.Keys = TestUtil.Ints(1, 2);
            initialState.Metadata = Metadata;
            var state = initialState.New();
            var sut = new CheckGameTurnLimit();

            sut.Resolve(state);

            StateAsserter.StateEquals(initialState, state);
        }

        [Test]
        public void GameTurnLimitReached()
        {
            var initialState = StateTestUtil.EmptyMutableState;
            initialState.TurnNumber = 10;
            initialState.Keys = TestUtil.Ints(1, 2);
            initialState.Metadata = Metadata;
            var state = initialState.New();
            var sut = new CheckGameTurnLimit();

            sut.Resolve(state);

            var expectedState = initialState.New(playerTurn: Player.Player2, isGameOver: true);
            StateAsserter.StateEquals(expectedState, state);
        }
    }
}