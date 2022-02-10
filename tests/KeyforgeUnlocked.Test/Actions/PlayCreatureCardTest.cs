using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using PlayCreatureCard = KeyforgeUnlocked.Actions.PlayCreatureCard;

namespace KeyforgeUnlockedTest.Actions
{
    [TestFixture]
    class PlayCreatureCardTest : ActionTestBase<PlayCreatureCard>
    {
        System.Action<CardNotPresentException> assert;

        static readonly ICreatureCard Card = new SampleCreatureCard();
        static readonly ICreatureCard otherCard1 = new SampleCreatureCard();
        static readonly ICreatureCard otherCard2 = new SampleCreatureCard();
        static readonly ICreatureCard otherCard3 = new SampleCreatureCard();
        static readonly IEffect unresolvedEffect = new DeclareHouse();

        [Test]
        public void Act_EmptyBoard()
        {
            var hands = TestUtil.Sets<ICard>(new[] { Card, otherCard1 }, new[] { otherCard2, otherCard3 });
            var effects = new LazyStackQueue<IEffect>(new[] { unresolvedEffect });
            var state = StateTestUtil.EmptyState.New(hands: hands, effects: effects);
            var sut = new PlayCreatureCard(null, Card, 0);

            var expectedHands = TestUtil.Sets<ICard>(new[] { otherCard1 }, new[] { otherCard2, otherCard3 });
            var expectedEffects = new LazyStackQueue<IEffect>(new[]
                { unresolvedEffect, new KeyforgeUnlocked.Effects.PlayCreatureCard(Card, 0) });
            var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects, hands: expectedHands);
            expectedState.HistoricData.ActionPlayedThisTurn = true;

            ActAndAssert(sut, state, expectedState);
        }

        [Test]
        public void Act_CardNotInHand_CardNotPresentException()
        {
            var state = StateTestUtil.EmptyMutableState;
            var sut = new PlayCreatureCard(null, Card, 0);

            assert = e => { Assert.True(e.Id.Equals(Card)); };

            ActExpectException(sut, state, assert);
        }
    }
}