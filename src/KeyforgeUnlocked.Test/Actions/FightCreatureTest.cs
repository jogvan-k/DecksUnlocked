using System;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using FightCreature = KeyforgeUnlocked.Actions.FightCreature;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class FightCreatureTest : ActionTestBase
  {
    const string FightingCreatureId = "id1";
    const string TargetCreatureId = "id2";
    readonly Creature _fightingCreature = CreatureTestUtil.SampleLogosCreature(FightingCreatureId, true);
    readonly Creature _targetCreature = CreatureTestUtil.SampleUntamedCreature(TargetCreatureId, true);
    readonly FightCreature sut;

    Action<CreatureNotPresentException> creatureNotPresentAsserts;
    Action<InvalidFightException> invalidFightException;
    Action<CreatureNotReadyException> creatureNotReadyException;

    public FightCreatureTest()
    {
      sut = new FightCreature(FightingCreatureId, TargetCreatureId);
    }

    [Test]
    public void FightingCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        Enumerable.Empty<Creature>(),
        new[] {_targetCreature});
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotPresentAsserts = e => { Assert.AreEqual(FightingCreatureId, e.CreatureId); };

      ActExpectException(sut, state, creatureNotPresentAsserts);
    }

    [Test]
    public void TargetCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        new[] {_fightingCreature},
        Enumerable.Empty<Creature>());
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotPresentAsserts = e => { Assert.AreEqual(TargetCreatureId, e.CreatureId); };

      ActExpectException(sut, state, creatureNotPresentAsserts);
    }

    [Test]
    public void OpponentsCreatureIsFighting_ValidationError()
    {
      var fields = TestUtil.Lists(
        Enumerable.Empty<Creature>(),
        new[] {_fightingCreature, _targetCreature});
      var state = StateTestUtil.EmptyState.New(fields: fields);

      invalidFightException = e =>
      {
        Assert.AreEqual(FightingCreatureId, e.FightingCreatureId);
        Assert.AreEqual(TargetCreatureId, e.TargetCreatureId);
      };

      ActExpectException(sut, state, invalidFightException);
    }

    [Test]
    public void PlayersCreatureIsTarget_ValidationError()
    {
      var fields = TestUtil.Lists(
        new[] {_fightingCreature, _targetCreature},
        Enumerable.Empty<Creature>());
      var state = StateTestUtil.EmptyState.New(fields: fields);

      invalidFightException = e =>
      {
        Assert.AreEqual(FightingCreatureId, e.FightingCreatureId);
        Assert.AreEqual(TargetCreatureId, e.TargetCreatureId);
      };

      ActExpectException(sut, state, invalidFightException);
    }

    [Test]
    public void FightingCreatureNotReady_validationError()
    {
      var fightingCreature = CreatureTestUtil.SampleLogosCreature(FightingCreatureId, false);
      var fields = TestUtil.Lists(fightingCreature,_targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotReadyException = e => { Assert.AreEqual(fightingCreature, e.Creature); };

      ActExpectException(sut, state, creatureNotReadyException);
    }

    [Test]
    public void ValidState()
    {
      var fields = TestUtil.Lists(_fightingCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      var expectedEffects = new StackQueue<IEffect>(new[] {new KeyforgeUnlocked.Effects.FightCreature(_fightingCreature, _targetCreature)});
      var expectedState = StateTestUtil.EmptyState.New(fields: fields, effects: expectedEffects);

      Act(sut, state, expectedState);
    }
  }
}