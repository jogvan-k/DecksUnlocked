using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class DiscardCardTest : ActionTestBase<BasicAction>
  {
    readonly ICard sampleCard = new SampleCreatureCard();

    [Test]
    public void Act_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new DiscardCard(null, sampleCard);

      var expectedState = StateTestUtil.EmptyMutableState;
      expectedState.Effects.Enqueue(new KeyforgeUnlocked.Effects.DiscardCard(sampleCard));
      expectedState.HistoricData.ActionPlayedThisTurn = true;

      Act(sut, state, expectedState);
    }
  }
}