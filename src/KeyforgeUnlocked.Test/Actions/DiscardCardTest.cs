using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class DiscardCardTest
  {
    readonly Card sampleCard = new SimpleCreatureCard();

    [Test]
    public void DoActionNoResolve_EmptyState()
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new DiscardCard(sampleCard);

      state = sut.DoActionNoResolve(state);

      var expectedState = TestUtil.EmptyMutableState;
      expectedState.Effects.Enqueue(new KeyforgeUnlocked.Effects.DiscardCard(sampleCard));
      Assert.AreEqual(expectedState, state);
    }
  }
}