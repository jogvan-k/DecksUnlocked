using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class UseCreatureGroupTest
  {
    SampleCreatureCard _sampleCreatureCard = new SampleCreatureCard();

    SampleCreatureCard _sampleCreatureCardWithCreatureAbility =
      new SampleCreatureCard(creatureAbility: Delegates.NoChange);

    Creature _opponentCreature1;
    Creature _opponentCreature2;

    public UseCreatureGroupTest()
    {
      _opponentCreature1 = new Creature(_sampleCreatureCard);
      _opponentCreature2 = new Creature(_sampleCreatureCard);
    }

    [Test]
    public void Actions_CreatureNotReady_NoActions()
    {
      var creature = new Creature(_sampleCreatureCardWithCreatureAbility);
      var field = TestUtil.Lists(creature, new Creature(_sampleCreatureCardWithCreatureAbility));
      var state = StateTestUtil.EmptyState.New(fields: field);
      var sut = new UseCreatureGroup(state, new Creature(_sampleCreatureCardWithCreatureAbility));

      var actions = sut.Actions;

      Assert.AreEqual(ImmutableList<Action>.Empty, actions);
    }

    [Test]
    public void Actions_CreatureReady()
    {
      var creature = new Creature(_sampleCreatureCard, isReady: true);
      var state = SetupState(creature);
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions;

      var expectedActions = ImmutableArray<Action>.Empty.AddRange(
        new[]
        {
          (Action) new FightCreature(creature, _opponentCreature1),
          new FightCreature(creature, _opponentCreature2),
          new Reap(creature)
        });
      Assert.AreEqual(expectedActions, actions);
    }

    [Test]
    public void Actions_CreatureReadyWithCreatureAbility()
    {
      var creature = new Creature(_sampleCreatureCardWithCreatureAbility, isReady: true);
      var state = SetupState(creature);
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions;

      var expectedActions = ImmutableArray<Action>.Empty.AddRange(
        new[]
        {
          (Action) new FightCreature(creature, _opponentCreature1),
          new FightCreature(creature, _opponentCreature2),
          new UseCreatureAbility(creature),
          new Reap(creature)
        });
      Assert.AreEqual(expectedActions, actions);
    }

    MutableState SetupState(Creature creature)
    {
      var opponentCreatures = new[]
      {
        _opponentCreature1,
        _opponentCreature2
      };
      var fields = TestUtil.Lists(new[] {creature}.AsEnumerable(), opponentCreatures);

      return StateTestUtil.EmptyState.New(fields: fields);
    }
  }
}