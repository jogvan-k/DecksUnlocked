using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.States;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class DiscardCardTest
  {
    Card Card = new SimpleCreatureCard();

    [Test]
    public void DoActionNoResolve_EmptyState()
    {
      IState state = TestUtil.EmptyMutableState.ResolveEffects();
      var sut = new DiscardCard(Card);

      state = sut.DoActionNoResolve(state);

      var expectedState = TestUtil.EmptyMutableState;
      expectedState.Effects.Enqueue(new KeyforgeUnlocked.Effects.DiscardCard(Card));
      Assert.AreEqual(expectedState, state);
    }
  }
}