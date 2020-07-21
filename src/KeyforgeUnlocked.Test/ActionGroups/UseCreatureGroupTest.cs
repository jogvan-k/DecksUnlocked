using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using FightCreature = KeyforgeUnlocked.Actions.FightCreature;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class UseCreatureGroupTest
  {
    [Test]
    public void Actions_CreatureNotReady_NoActions()
    {
      var creature = new Creature(new SampleCreatureCard());
      var field = TestUtil.Lists(creature, new Creature(new SampleCreatureCard()));
      var state = StateTestUtil.EmptyState.New(fields: field);
      var sut = new UseCreatureGroup(state, new Creature(new SampleCreatureCard()));

      var actions = sut.Actions;

      Assert.AreEqual(ImmutableList<Action>.Empty, actions);
    }

    [Test]
    public void Actions_CreatureReady()
    {
      var creature = new Creature(new SampleCreatureCard(), isReady: true);
      var opponentCreature1 = new Creature(new SampleCreatureCard());
      var opponentCreature2 = new Creature(new SampleCreatureCard());
      var opponentCreatures = new[]
      {
        opponentCreature1,
        opponentCreature2
      };
      var fields = TestUtil.Lists(new[] {creature}.AsEnumerable(), opponentCreatures);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions;

      var expectedActions = ImmutableArray<Action>.Empty.AddRange(
        new[]
        {
          (Action) new FightCreature(creature.Id, opponentCreature1.Id),
          new FightCreature(creature.Id, opponentCreature2.Id),
          new Reap(creature.Id)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}