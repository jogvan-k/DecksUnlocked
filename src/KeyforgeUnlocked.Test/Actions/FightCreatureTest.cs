using System.Linq;
using KeyforgeUnlocked.Cards;
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
  sealed class FightCreatureTest : ActionTestBase<FightCreature>
  {
    static readonly ICreatureCard FightingCreatureCard = new SampleCreatureCard(House.Brobnar, id: "fighter");
    static readonly ICreatureCard TargetCreatureCard = new SampleCreatureCard(id: "target");
    static readonly Creature _fightingCreature;
    static readonly Creature _targetCreature;
    static readonly FightCreature sut;

    System.Action<CreatureNotPresentException> creatureNotPresentAsserts;
    System.Action<InvalidFightException> invalidFightException;
    System.Action<CreatureNotReadyException> creatureNotReadyException;
    System.Action<CreatureStunnedException> creatureStunnedException;

    static FightCreatureTest()
    {
      _fightingCreature = new Creature(FightingCreatureCard, isReady: true);
      _targetCreature = new Creature(TargetCreatureCard, isReady: true);
      sut = new FightCreature(null, _fightingCreature, _targetCreature);
    }

    [Test]
    public void FightingCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        Enumerable.Empty<Creature>(),
        new[] {_targetCreature});
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);

      creatureNotPresentAsserts = e => { Assert.True(e.Id.Equals(_fightingCreature)); };

      ActExpectException(sut, state, creatureNotPresentAsserts);
    }

    [Test]
    public void TargetCreatureNotPresent_ValidationError()
    {
      var fields = TestUtil.Lists(
        new[] {_fightingCreature},
        Enumerable.Empty<Creature>());
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);

      creatureNotPresentAsserts = e => { Assert.True(e.Id.Equals(_targetCreature)); };

      ActExpectException(sut, state, creatureNotPresentAsserts);
    }

    [Test]
    public void OpponentsCreatureIsFighting_ValidationError()
    {
      var fields = TestUtil.Lists(
        Enumerable.Empty<Creature>(),
        new[] {_fightingCreature, _targetCreature});
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);

      invalidFightException = e =>
      {
        Assert.AreEqual(_fightingCreature, e.FightingCreature);
        Assert.AreEqual(_targetCreature, e.TargetCreature);
      };

      ActExpectException(sut, state, invalidFightException);
    }

    [Test]
    public void PlayersCreatureIsTarget_InvalidFightException()
    {
      var fields = TestUtil.Lists(
        new[] {_fightingCreature, _targetCreature},
        Enumerable.Empty<Creature>());
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);

      invalidFightException = e =>
      {
        Assert.AreEqual(_fightingCreature, e.FightingCreature);
        Assert.AreEqual(_targetCreature, e.TargetCreature);
      };

      ActExpectException(sut, state, invalidFightException);
    }

    [Test]
    public void FightingCreatureNotReady_CreatureNotReadyException()
    {
      var unreadyCreature = new Creature(FightingCreatureCard);
      var fields = TestUtil.Lists(unreadyCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);
      var sut = new FightCreature(null, unreadyCreature, _targetCreature);

      creatureNotReadyException = e => { Assert.AreEqual(unreadyCreature, e.Creature); };

      ActExpectException(sut, state, creatureNotReadyException);
    }

    [Test]
    public void FightingCreatureStunned_FightingCreatureStunnedException()
    {
      var fightingCreature = new Creature(FightingCreatureCard, isReady: true, state: CreatureState.Stunned);
      var fields = TestUtil.Lists(fightingCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);
      var sut = new FightCreature(null, fightingCreature, _targetCreature);

      creatureStunnedException = e => Assert.AreEqual(fightingCreature, e.Creature);

      ActExpectException(sut, state, creatureStunnedException);
    }

    [Test]
    public void ValidState()
    {
      var fields = TestUtil.Lists(_fightingCreature, _targetCreature);
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);

      var expectedEffects = new LazyStackQueue<IEffect>(
        new[] {new KeyforgeUnlocked.Effects.FightCreature(_fightingCreature, _targetCreature)});
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: House.Brobnar, fields: fields, effects: expectedEffects);
      expectedState.HistoricData.ActionPlayedThisTurn = true;

      ActAndAssert(sut, state, expectedState);
    }
  }
}