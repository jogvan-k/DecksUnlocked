using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class PlayCreatureCardGroupTest
  {
    static readonly CreatureCard Card = new SampleCreatureCard();

    [Test]
    public void Actions_EmptyState()
    {
      IState state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedAction =
        ImmutableList<Action>.Empty.Add(new PlayCreatureCard(Card, 0)).Add(new DiscardCard(Card));
      Assert.AreEqual(expectedAction, actions);
    }

    [Test]
    public void Actions_CreaturesOnBoard_ActionsOnlyOnFlank()
    {
      var state = StateTestUtil.EmptyMutableState;
      for (int i = 0; i < 5; i++)
        state.Fields[Player.Player1].Add(new Creature(new SampleCreatureCard()));
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedActions =
        ImmutableList<Action>.Empty
          .Add(new PlayCreatureCard(Card, 0))
          .Add(new PlayCreatureCard(Card, 5))
          .Add(new DiscardCard(Card));
      Assert.AreEqual(expectedActions, actions);
    }
  }
}