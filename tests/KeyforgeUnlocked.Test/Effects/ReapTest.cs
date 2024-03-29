using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
    [TestFixture]
    class ReapTest
    {
        bool _reapAbilityResolved;
        readonly ICreatureCard _creatureCard;
        static readonly ICreatureCard OtherCreatureCard1 = new SampleCreatureCard();
        static readonly ICreatureCard OtherCreatureCard2 = new SampleCreatureCard();
        readonly Creature _creature;
        Reap _sut;

        public ReapTest()
        {
            _reapAbilityResolved = false;
            Callback reapAbility = (_, _, _) => _reapAbilityResolved = true;
            _creatureCard = new SampleCreatureCard(reapAbility: reapAbility);
            _creature = new Creature(_creatureCard, isReady: true);
            _sut = new Reap(_creature);
        }

        [Test]
        public void Resolve_EmptyBoard_CreatureNotPresentException()
        {
            var state = StateTestUtil.EmptyMutableState;

            try
            {
                _sut.Resolve(state);
            }
            catch (CreatureNotPresentException e)
            {
                Assert.That(e.Id.Equals(_creature));
                StateAsserter.StateEquals(state, e.State);
                return;
            }

            Assert.Fail();
        }

        [Test]
        public void Resolve_CreatureNotReady_CreatureNotReadyException()
        {
            var state = StateTestUtil.EmptyState.New(
                fields: new Dictionary<Player, IMutableList<Creature>>
                {
                    {
                        Player.Player1,
                        new LazyList<Creature> { new Creature(_creatureCard) }
                    },
                    { Player.Player2, new LazyList<Creature>() }
                }.ToImmutableDictionary());
            var sut = new Reap(new Creature(_creatureCard));

            try
            {
                sut.Resolve(state);
            }
            catch (CreatureNotReadyException e)
            {
                Assert.AreEqual(new Creature(_creatureCard), e.Creature);
                StateAsserter.StateEquals(state, e.State);
                return;
            }

            Assert.Fail();
        }

        [Test]
        public void Resolve_FieldWithCreatures()
        {
            var state = StateTestUtil.EmptyState.New(
                fields: new Dictionary<Player, IMutableList<Creature>>
                {
                    {
                        Player.Player1,
                        new LazyList<Creature>
                        {
                            new Creature(_creatureCard, isReady: true),
                            new Creature(OtherCreatureCard1, isReady: true)
                        }
                    },
                    {
                        Player.Player2,
                        new LazyList<Creature>
                        {
                            new Creature(OtherCreatureCard2, isReady: true)
                        }
                    }
                }.ToImmutableDictionary());

            _sut.Resolve(state);

            var expectedField = new Dictionary<Player, IMutableList<Creature>>
            {
                {
                    Player.Player1,
                    new LazyList<Creature>
                    {
                        new Creature(_creatureCard),
                        new Creature(OtherCreatureCard1, isReady: true)
                    }
                },
                {
                    Player.Player2,
                    new LazyList<Creature>
                    {
                        new Creature(OtherCreatureCard2, isReady: true)
                    }
                }
            }.ToImmutableDictionary();
            var expectedResolvedEffects = new List<IResolvedEffect> { new Reaped(new Creature(_creature.Card)) };
            var expectedAember = new Dictionary<Player, int>
                { { Player.Player1, 1 }, { Player.Player2, 0 } }.ToLookup();
            var expectedState = StateTestUtil.EmptyMutableState.New(
                fields: expectedField, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects),
                aember: expectedAember);

            StateAsserter.StateEquals(expectedState, state);
            Assert.True(_reapAbilityResolved);
        }
    }
}