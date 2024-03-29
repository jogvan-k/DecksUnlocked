using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
    [TestFixture]
    class DeclareHouseTest
    {
        static readonly House[] Player1Houses = { House.Brobnar, House.Logos, House.Sanctum };
        static readonly House[] Player2Houses = { House.Dis, House.Mars, House.Saurian };

        static readonly ImmutableLookup<Player, IImmutableSet<House>> Houses =
            new ImmutableLookup<Player, IImmutableSet<House>>(new Dictionary<Player, IImmutableSet<House>>
            {
                { Player.Player1, Player1Houses.ToImmutableHashSet() },
                { Player.Player2, Player2Houses.ToImmutableHashSet() }
            });

        static readonly DeclareHouse Sut = new();

        [Test]
        public void EmptyState_ThrowException()
        {
            var state = StateTestUtil.EmptyMutableState;
            state.Metadata = null!;

            try
            {
                Sut.Resolve(state);
            }
            catch (NoMetadataException e)
            {
                Assert.AreEqual(state, e.State);
                return;
            }

            Assert.Fail();
        }

        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void StateWithMetadata(Player playerTurn)
        {
            var metadata = new Metadata(ImmutableLookup<Player, IImmutableList<ICard>>.Empty, Houses, 0, 0);
            var state = StateTestUtil.EmptyState.New(playerTurn: playerTurn, metadata: metadata);

            Sut.Resolve(state);

            var expectedHouses = playerTurn == Player.Player1 ? Player1Houses : Player2Houses;
            var expectedActionGroups = new List<IActionGroup> { new DeclareHouseGroup(expectedHouses) };
            var expectedState = StateTestUtil.EmptyState.New(
                playerTurn: playerTurn, actionGroups: new LazyList<IActionGroup>(expectedActionGroups),
                metadata: metadata);
            StateAsserter.StateEquals(expectedState, state);
        }
    }
}