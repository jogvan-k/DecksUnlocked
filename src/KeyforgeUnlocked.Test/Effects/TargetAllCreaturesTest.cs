using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class TargetAllCreaturesTest
  {
    Creature playerOneCreature = new Creature(new SampleCreatureCard());
    Creature playerTwoCreature = new Creature(new SampleCreatureCard());

    List<IIdentifiable> _targetedCreatures;
    Callback _effect;

    [SetUp]
    public void SetUp()
    {
      _targetedCreatures = new List<IIdentifiable>();
      _effect = (s, c, _) => _targetedCreatures.Add(c);
    }

    [Test]
    public void Resolve_NoValidTargets()
    {
      var sut = Setup(out var state, (s, c) => false);

      sut.Resolve(state);

      var expectedState = State();
      StateAsserter.StateEquals(expectedState, state);
      Assert.IsEmpty(_targetedCreatures);
    }

    [Test]
    public void Resolve_TargetSingleCreature()
    {
      var sut = Setup(out var state, (s, c) => ((Object) c).Equals(playerOneCreature));

      sut.Resolve(state);

      var expectedState = State();
      StateAsserter.StateEquals(expectedState, state);
      Assert.AreEqual(playerOneCreature, _targetedCreatures.Single());
    }

    [Test]
    public void Resolve_TargetAllCreatures()
    {
      var sut = Setup(out var state, (s, c) => true);

      sut.Resolve(state);

      var expectedState = State();
      StateAsserter.StateEquals(expectedState, state);
      Assert.That(_targetedCreatures, Is.EquivalentTo(new []{playerOneCreature, playerTwoCreature}));
    }

    TargetAllCreatures Setup(out IMutableState state, ValidOn validOn)
    {
      state = State();
      return new TargetAllCreatures(_effect, validOn);
    }

    IMutableState State()
    {
      var fields = TestUtil.Lists(playerOneCreature, playerTwoCreature);
      return StateTestUtil.EmptyState.New(fields: fields);
    }
  }
}