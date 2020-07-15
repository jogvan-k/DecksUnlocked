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
    readonly Card sampleCard = new SimpleCreatureCard();

    [Test]
    public void Act_EmptyState()
    {
      var state = StateUtil.EmptyMutableState;
      var sut = new DiscardCard(sampleCard);

      Act(sut, state);

      var expectedState = StateUtil.EmptyMutableState;
      expectedState.Effects.Enqueue(new KeyforgeUnlocked.Effects.DiscardCard(sampleCard));
      Assert.AreEqual(expectedState, state);
    }
  }
}