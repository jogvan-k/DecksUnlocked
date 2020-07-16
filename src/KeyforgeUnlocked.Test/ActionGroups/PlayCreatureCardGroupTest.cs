using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class PlayCreatureCardGroupTest
  {
    static readonly SimpleCreatureCard Card = new SimpleCreatureCard();

    [Test]
    public void Actions_EmptyState()
    {
      IState state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedAction =
        ImmutableHashSet<Action>.Empty.Add(new PlayCreature(Card, 0)).Add(new DiscardCard(Card));
      Assert.AreEqual(expectedAction, actions);
    }

    [Test]
    public void Actions_CreaturesOnBoard_ActionsOnlyOnFlank()
    {
      IState state = StateTestUtil.EmptyMutableState;
      for (int i = 0; i < 5; i++)
        state.Fields[Player.Player1].Add(new Creature(1, 0, new SimpleCreatureCard()));
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedActions =
        ImmutableHashSet<Action>.Empty
          .Add(new PlayCreature(Card, 0))
          .Add(new PlayCreature(Card, 5))
          .Add(new DiscardCard(Card));
      Assert.AreEqual(expectedActions, actions);
    }
  }
}