using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using PlayActionCard = KeyforgeUnlocked.Actions.PlayActionCard;

namespace KeyforgeUnlockedTest.Actions
{
    [TestFixture]
    sealed class PlayActionCardTest : ActionTestBase<PlayActionCard>
    {
        static IActionCard sampleCard = new SampleActionCard();
        static PlayActionCard _sut = new(null, sampleCard);
        static IEffect unresolvedEffect = new EndTurn();

        [Test]
        public void Act()
        {
            var hands = TestUtil.Sets<ICard>(sampleCard);
            var effects = new LazyStackQueue<IEffect>(new[] { unresolvedEffect });
            var state = StateTestUtil.EmptyMutableState.New(hands: hands, effects: effects);

            var expectedEffects = new LazyStackQueue<IEffect>(new[]
                { unresolvedEffect, new KeyforgeUnlocked.Effects.PlayActionCard(sampleCard) });
            var expectedState = StateTestUtil.EmptyState.New(effects: expectedEffects);
            expectedState.HistoricData.ActionPlayedThisTurn = true;
            ActAndAssert(_sut, state, expectedState);
        }

        [Test]
        public void Act_CardNotInHand_CardNotPresentException()
        {
            var state = StateTestUtil.EmptyMutableState;

            try
            {
                ActAndAssert(_sut, state, null);
            }
            catch (CardNotPresentException e)
            {
                Assert.True(e.Id.Equals(sampleCard));
                return;
            }

            Assert.Fail();
        }
    }
}