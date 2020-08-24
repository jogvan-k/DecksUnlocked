using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class DiscardCardTest : ActionTestBase
  {
    readonly Card sampleCard = new SampleCreatureCard();

    [Test]
    public void Act_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new DiscardCard(sampleCard);

      var expectedState = StateTestUtil.EmptyMutableState;
      expectedState.Effects.Enqueue(new KeyforgeUnlocked.Effects.DiscardCard(sampleCard));

      Act(sut, state, expectedState);
    }
  }
}