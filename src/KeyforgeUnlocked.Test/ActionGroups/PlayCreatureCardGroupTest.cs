using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
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
      IState state = TestUtil.EmptyMutableState;
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedAction = new PlayCreature(Card, 0);
      var action = (PlayCreature) actions.Single();
      Assert.AreEqual(expectedAction, action);
    }

    [Test]
    public void Actions_CreaturesOnBoard_ActionsOnlyOnFlank()
    {
      IState state = TestUtil.EmptyMutableState;
      for (int i = 0; i < 5; i++)
        state.Fields[Player.Player1].Add(new Creature(1, 0, new SimpleCreatureCard()));
      var sut = new PlayCreatureCardGroup(state, Card);

      var actions = sut.Actions;

      var expectedActions =
        ImmutableList<Action>.Empty.AddRange(
          new[] {new PlayCreature(Card, 0),
            new PlayCreature(Card, 5)});
      Assert.AreEqual(expectedActions, actions);
    }
  }
}