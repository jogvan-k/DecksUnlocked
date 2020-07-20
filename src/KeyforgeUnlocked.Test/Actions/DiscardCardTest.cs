using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class DiscardCardTest : ActionTestBase
  {
    readonly Card sampleCard = new LogosCreatureCard();

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