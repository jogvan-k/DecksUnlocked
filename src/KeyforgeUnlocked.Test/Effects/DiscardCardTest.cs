using KeyforgeUnlocked.Cards;
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
  class DiscardCardTest
  {
    ICard _sampleCard = new SampleCreatureCard();

    [Test]
    public void Resolve_SampleSetup()
    {
      var hands = SampleSets.SampleHands;
      hands[Player.Player1].Add(_sampleCard);
      var state = StateTestUtil.EmptyMutableState.New(hands: hands);
      var sut = new DiscardCard(_sampleCard);

      sut.Resolve(state);

      var expectedHands = SampleSets.SampleHands;
      var expectedDiscards = TestUtil.Sets(_sampleCard);
      var expectedState = StateTestUtil.EmptyMutableState.New(
        discards: expectedDiscards,
        hands: expectedHands,
        resolvedEffects: new LazyList<IResolvedEffect> {new CardDiscarded(_sampleCard)});
      expectedState.HistoricData.CardsDiscardedThisTurn = expectedState.HistoricData.CardsDiscardedThisTurn.Add(_sampleCard);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_CardNotInHand_ThrowException()
    {
      var hands = SampleSets.SampleHands;
      var state = StateTestUtil.EmptyMutableState.New(hands: hands);
      var sut = new DiscardCard(_sampleCard);

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