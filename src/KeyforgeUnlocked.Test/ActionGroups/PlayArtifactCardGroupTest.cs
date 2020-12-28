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
  sealed class PlayArtifactCardGroupTest
  {
    IArtifactCard sampleCard = new SampleArtifactCard();
    ImmutableState _state = StateTestUtil.EmptyState;

    [Test]
    public void Tests_EmptyState()
    {
      var sut = new PlayArtifactCardGroup(sampleCard);

      var actions = sut.Actions(_state);

      var expectedActions = ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new PlayArtifactCard(_state, sampleCard), new DiscardCard(_state, sampleCard)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}