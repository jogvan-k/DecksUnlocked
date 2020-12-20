using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  sealed class PlayActionCardGroupTest
  {
    ActionCard sampleCard = new SampleActionCard();
    private ImmutableState _state = StateTestUtil.EmptyState;

    [Test]
    public void Tests_EmptyState()
    {
      var sut = new PlayActionCardGroup(sampleCard);

      var actions = sut.Actions(_state);

      var expectedActions = ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new PlayActionCard(_state, sampleCard), new DiscardCard(_state, sampleCard)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}