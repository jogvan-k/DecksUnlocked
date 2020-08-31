using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using PlayActionCard = KeyforgeUnlocked.Actions.PlayActionCard;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class PlayActionCardTest : ActionTestBase
  {
    static ActionCard sampleCard = new SampleActionCard();
    static PlayActionCard _sut = new PlayActionCard(null, sampleCard);
    static IEffect unresolvedEffect = new EndTurn();

    [Test]
    public void Act()
    {
      var hands = TestUtil.Sets<Card>(sampleCard);
      var effects = new StackQueue<IEffect>(new []{unresolvedEffect});
      var state = StateTestUtil.EmptyMutableState.New(hands: hands, effects: effects);

      var expectedEffects = new StackQueue<IEffect>(new[] {unresolvedEffect, new KeyforgeUnlocked.Effects.PlayActionCard(sampleCard)});
      var expectedState = StateTestUtil.EmptyState.New(effects: expectedEffects);
      Act(_sut, state, expectedState);
    }

    [Test]
    public void Act_CardNotInHand_CardNotPresentException()
    {
      var state = StateTestUtil.EmptyMutableState;

      try
      {
        Act(_sut, state, null);
      }
      catch (CardNotPresentException e)
      {
        Assert.AreEqual(sampleCard.Id, e.CardId);
        return;
      }

      Assert.Fail();
    }
  }
}