using System;
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
  sealed class UseCreatureGroupTest
  {
    SampleCreatureCard _sampleCreatureCard = new();

    SampleCreatureCard _sampleCreatureCardWithCreatureAbility =
      new SampleCreatureCard(creatureAbility: Delegates.NoChange);

    Creature _opponentCreature1;
    Creature _opponentCreature2;
    Creature _opponentCreature3;
    Creature _opponentCreatureWithTaunt1;
    Creature _opponentCreatureWithTaunt2;

    public UseCreatureGroupTest()
    {
      _opponentCreature1 = new Creature(new SampleCreatureCard());
      _opponentCreature2 = new Creature(new SampleCreatureCard());
      _opponentCreature3 = new Creature(new SampleCreatureCard());
      _opponentCreatureWithTaunt1 = new Creature(new SampleCreatureCard(keywords: new[] {Keyword.Taunt}));
      _opponentCreatureWithTaunt2 = new Creature(new SampleCreatureCard(keywords: new[] {Keyword.Taunt}));
    }

    [Test]
    public void Actions_CreatureNotReady_NoActions()
    {
      var field = TestUtil.Lists(new Creature(_sampleCreatureCard));
      var state = StateTestUtil.EmptyState.New(fields: field).ToImmutable();
      var sut = new UseCreatureGroup(state, new Creature(_sampleCreatureCardWithCreatureAbility));

      var actions = sut.Actions(state);

      Assert.AreEqual(ImmutableList<IAction>.Empty, actions);
    }

    [Test]
    public void Actions_CreatureReady()
    {
      var creature = new Creature(_sampleCreatureCard, isReady: true);
      var state = SetupState(creature);
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions(state);

      var expectedActions = ImmutableArray<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new FightCreature(state, creature, _opponentCreature1),
          new FightCreature(state, creature, _opponentCreature2),
          new Reap(state, creature)
        });
      Assert.AreEqual(expectedActions, actions);
    }

    [Test]
    public void Actions_OpponentWithTaunt_AttackActionsOnlyOnValidCreatures()
    {
      var creature = new Creature(_sampleCreatureCard, isReady: true);
      var field = TestUtil.Lists(
        new[] {creature}.AsEnumerable(),
        new[] {_opponentCreature1, _opponentCreatureWithTaunt1, _opponentCreatureWithTaunt2, _opponentCreature2, _opponentCreature3});
      var state = StateTestUtil.EmptyState.New(fields: field).ToImmutable();
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions(state);

      var expectedActions = ImmutableArray<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new FightCreature(state, creature, _opponentCreatureWithTaunt1),
          new FightCreature(state, creature, _opponentCreatureWithTaunt2),
          new FightCreature(state, creature, _opponentCreature3),
          new Reap(state, creature)
        });
      Assert.AreEqual(expectedActions, actions);
    }

    [Test, Combinatorial]
    public void Actions_CreatureReadyWithCreatureAbility(
      [Values(UseCreature.None,
        UseCreature.Fight,
        UseCreature.Fight | UseCreature.Reap,
        UseCreature.Fight | UseCreature.ActiveAbility,
        UseCreature.Reap,
        UseCreature.Reap | UseCreature.ActiveAbility,
        UseCreature.ActiveAbility,
        UseCreature.All)] UseCreature useCreature,
      [Values(false, true)]bool creatureStunned)
    {
      var creature = new Creature(_sampleCreatureCardWithCreatureAbility, isReady: true, state: creatureStunned ? CreatureState.Stunned : CreatureState.None);
      var state = SetupState(creature);
      var sut = new UseCreatureGroup(state, creature, allowedUsages: useCreature);

      var actions = sut.Actions(state);

      var expectedActions = ImmutableArray<IAction>.Empty;

      if (creatureStunned)
      {
        if (useCreature == UseCreature.All)
          expectedActions = expectedActions.Add(new RemoveStun(state, creature, true));
      }
      else
      {
        if ((useCreature & UseCreature.Fight) != 0)
        {
          expectedActions = expectedActions.AddRange(
            new[]
            {
              (IAction) new FightCreature(state, creature, _opponentCreature1),
              new FightCreature(state, creature, _opponentCreature2)
            });
        }

        if ((useCreature & UseCreature.ActiveAbility) != 0)
        {
          expectedActions = expectedActions.Add(new UseCreatureAbility(state, creature));
        }

        if ((useCreature & UseCreature.Reap) != 0)
        {
          expectedActions = expectedActions.Add(new Reap(state, creature));
        }
      }

      Assert.AreEqual(expectedActions, actions);
    }

    [Test]
    public void Actions_CreatureStunned_OnlyRemoveStunAction()
    {
      var creature = new Creature(_sampleCreatureCardWithCreatureAbility, isReady: true, state: CreatureState.Stunned);
      var state = SetupState(creature);
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions(state);

      var expectedActions = ImmutableArray<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new RemoveStun(state, creature)
        });
      Assert.AreEqual(expectedActions, actions);
    }

    [Test]
    public void Actions_ConditionalActionAllowed(
      [Values(typeof(FightCreature), typeof(Reap), typeof(UseCreatureAbility))]Type type)
    {
      ActionPredicate actionPredicate = (_, a) => a.GetType().IsAssignableTo(type);
      var creatureCard = new SampleCreatureCard(creatureAbility: Delegates.NoChange, useActionAllowed: actionPredicate);
      var creature = new Creature(creatureCard, isReady: true);
      var field = TestUtil.Lists(
        new[] {creature}.AsEnumerable(),
        new[] {_opponentCreature1});
      var state = StateTestUtil.EmptyState.New(fields: field).ToImmutable();
      var sut = new UseCreatureGroup(state, new Creature(creatureCard, isReady: true));

      var actions = sut.Actions(state);

      var expectedActions = ImmutableList<IAction>.Empty;
      var actionName = type.Name;
      if (actionName == "FightCreature")
        expectedActions = expectedActions.Add(new FightCreature(state, creature, _opponentCreature1));
      else if (actionName == "Reap")
        expectedActions = expectedActions.Add(new Reap(state, creature));
      else if (actionName == "UseCreatureAbility")
        expectedActions = expectedActions.Add(new UseCreatureAbility(state, creature));
      
      Assert.AreEqual(expectedActions, actions);
    }

    [Test]
    public void Actions_NoActionsAllowed()
    {
      var creatureCard = new SampleCreatureCard(creatureAbility: Delegates.NoChange, useActionAllowed: (_, _) => false);
      var creature = new Creature(creatureCard, isReady: true);
      
      var field = TestUtil.Lists(
        new[] {creature}.AsEnumerable(),
        new[] {_opponentCreature1});
      
      var state = StateTestUtil.EmptyState.New(fields: field).ToImmutable();
      var sut = new UseCreatureGroup(state, creature);

      var actions = sut.Actions(state);

      Assert.AreEqual(ImmutableList<IAction>.Empty, actions);
    }

    ImmutableState SetupState(Creature creature)
    {
      var opponentCreatures = new[]
      {
        _opponentCreature1,
        _opponentCreature2
      };
      var fields = TestUtil.Lists(new[] {creature}.AsEnumerable(), opponentCreatures);

      return StateTestUtil.EmptyState.New(fields: fields).ToImmutable();
    }
  }
}