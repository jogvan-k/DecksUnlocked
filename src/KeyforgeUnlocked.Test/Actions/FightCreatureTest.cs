using System;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
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
    static readonly CreatureCard FightingCreatureCard = new SampleCreatureCard();
    static readonly CreatureCard TargetCreatureCard = new SampleCreatureCard();
    static readonly Creature _fightingCreature = new Creature(FightingCreatureCard, isReady: true);
    static readonly Creature _targetCreature = new Creature(TargetCreatureCard, isReady: true);
    static readonly FightCreature sut;

    Action<CreatureNotPresentException> creatureNotPresentAsserts;
    Action<InvalidFightException> invalidFightException;
    Action<CreatureNotReadyException> creatureNotReadyException;

    static FightCreatureTest()
    {
      sut = new FightCreature(FightingCreatureCard.Id, TargetCreatureCard.Id);
    }

    [Test]
    public void FightingCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        Enumerable.Empty<Creature>(),
        new[] {_targetCreature});
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotPresentAsserts = e => { Assert.AreEqual(_fightingCreature.Id, e.CreatureId); };

      ActExpectException(sut, state, creatureNotPresentAsserts);
    }

    [Test]
    public void TargetCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        new[] {_fightingCreature},
        Enumerable.Empty<Creature>());
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotPresentAsserts = e => { Assert.AreEqual(_targetCreature.Id, e.CreatureId); };

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
        Assert.AreEqual(_fightingCreature.Id, e.FightingCreatureId);
        Assert.AreEqual(_targetCreature.Id, e.TargetCreatureId);
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
        Assert.AreEqual(_fightingCreature.Id, e.FightingCreatureId);
        Assert.AreEqual(_targetCreature.Id, e.TargetCreatureId);
      };

      ActExpectException(sut, state, invalidFightException);
    }

    [Test]
    public void FightingCreatureNotReady_validationError()
    {
      var unreadyCreature = new Creature(FightingCreatureCard);
      var fields = TestUtil.Lists(unreadyCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      creatureNotReadyException = e => { Assert.AreEqual(unreadyCreature, e.Creature); };

      ActExpectException(sut, state, creatureNotReadyException);
    }

    [Test]
    public void ValidState()
    {
      var fields = TestUtil.Lists(_fightingCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      var expectedEffects = new StackQueue<IEffect>(
        new[] {new KeyforgeUnlocked.Effects.FightCreature(_fightingCreature, _targetCreature)});
      var expectedState = StateTestUtil.EmptyState.New(fields: fields, effects: expectedEffects);

      Act(sut, state, expectedState);
    }
  }
}