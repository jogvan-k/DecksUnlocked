using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  sealed class PlayActionCardGroupTest
  {
    IActionCard sampleCard = new SampleActionCard();
    ImmutableState _state = StateTestUtil.EmptyState;

    [Test]
    public void Actions_EmptyState()
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
    
    [Test]
    public void Actions_PlayNotAllowed()
    {
      var card = new SampleActionCard(playAllowed: (_, _) => false);
      
      var sut = new PlayActionCardGroup(card);

      var actions = sut.Actions(_state);

      var expectedActions = ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          new DiscardCard(_state, card)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}