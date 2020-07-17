using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using DiscardCard = KeyforgeUnlocked.Effects.DiscardCard;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DiscardCardTest
  {
    Card sampleCard = new LogosCreatureCard();

    [Test]
    public void Resolve_SampleSetup()
    {
      var hands = SampleSets.SampleHands;
      hands[Player.Player1].Add(sampleCard);
      var state = StateTestUtil.EmptyMutableState.New(hands: hands);
      var sut = new DiscardCard(sampleCard);

      sut.Resolve(state);

      var expectedHands = SampleSets.SampleHands;
      var expectedDiscards = new Dictionary<Player, ISet<Card>>
      {
        {Player.Player1, new HashSet<Card> {sampleCard}},
        {Player.Player2, new HashSet<Card>()}
      };
      var expectedState = StateTestUtil.EmptyMutableState.New(
        discards: expectedDiscards,
        hands: expectedHands,
        resolvedEffects: new List<IResolvedEffect> {new KeyforgeUnlocked.ResolvedEffects.CardDiscarded(sampleCard)});
      Assert.AreEqual(expectedState, state);
    }

    [Test]
    public void Resolve_CardNotInHand_ThrowException()
    {
      var hands = SampleSets.SampleHands;
      var state = StateTestUtil.EmptyMutableState.New(hands: hands);
      var sut = new DiscardCard(sampleCard);

      try
      {
        sut.Resolve(state);
      }
      catch (CardNotPresentException e)
      {
        Assert.AreEqual(state, e.State);
        return;
      }

      Assert.Fail();
    }
  }
}