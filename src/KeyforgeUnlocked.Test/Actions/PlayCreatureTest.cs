using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using PlayCreatureCard = KeyforgeUnlocked.Actions.PlayCreatureCard;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class PlayCreatureTest : ActionTestBase
  {
    Action<CardNotPresentException> assert;

    static readonly CreatureCard Card = new SampleCreatureCard();
    static readonly CreatureCard otherCard1 = new SampleCreatureCard();
    static readonly CreatureCard otherCard2 = new SampleCreatureCard();
    static readonly CreatureCard otherCard3 = new SampleCreatureCard();
    static readonly IEffect unresolvedEffect = new DeclareHouse();

    [Test]
    public void Act_EmptyBoard()
    {
      var hands = TestUtil.Sets<Card>(new[] {Card, otherCard1}, new[] {otherCard2, otherCard3});
      var effects = new LazyStackQueue<IEffect>(new[] {unresolvedEffect});
      var state = StateTestUtil.EmptyState.New(hands: hands, effects: effects);
      var sut = new PlayCreatureCard(null, Card, 0);

      var expectedHands = TestUtil.Sets<Card>(new[] {otherCard1}, new[] {otherCard2, otherCard3});
      var expectedEffects = new LazyStackQueue<IEffect>(new[]
        {unresolvedEffect, new KeyforgeUnlocked.Effects.PlayCreatureCard(Card, 0)});
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects, hands: expectedHands);

      Act(sut, state, expectedState);
    }

    [Test]
    public void Act_CardNotInHand_CardNotPresentException()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreatureCard(null, Card, 0);

      assert = e => { Assert.AreEqual(Card.Id, e.CardId); };

      ActExpectException(sut, state, assert);
    }
  }
}