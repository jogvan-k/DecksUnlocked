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
    public void Actions_EmptyState()
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
    
    [Test]
    public void Actions_PlayNotAllowed()
    {
      var card = new SampleArtifactCard(playAllowed: (_, _) => false);
      var sut = new PlayArtifactCardGroup(card);

      var actions = sut.Actions(_state);

      var expectedActions = ImmutableList<IAction>.Empty.Add(new DiscardCard(_state, card));
      Assert.AreEqual(expectedActions, actions);
    }
  }
}