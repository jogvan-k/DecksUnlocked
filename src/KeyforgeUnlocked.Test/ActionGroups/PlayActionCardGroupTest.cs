using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  sealed class PlayActionCardGroupTest
  {
    ActionCard sampleCard = new SampleActionCard();

    [Test]
    public void Tests_EmptyState()
    {
      var sut = new PlayActionCardGroup(sampleCard);

      var actions = sut.Actions;

      var expectedActions = ImmutableList<Action>.Empty.AddRange(
        new[]
        {
          (Action) new PlayActionCard(sampleCard), new DiscardCard(sampleCard)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}