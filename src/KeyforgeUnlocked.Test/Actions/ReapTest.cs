using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class ReapTest : ActionTestBase<BasicAction>
  {
    const House ActiveHouse = House.Logos;
    readonly CreatureCard _creatureCard = new SampleCreatureCard(house: ActiveHouse);
    Creature _creature;
    IImmutableDictionary<Player, IMutableList<Creature>> _fields;

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var sut = Setup(false, ActiveHouse, false, out var state);

      System.Action<CreatureNotReadyException> asserts = e => Assert.AreEqual(_creature, e.Creature);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_CreatureReady()
    {
      var sut = Setup(true, ActiveHouse, false, out var state);

      var expectedState = Expected();

      Act(sut, state, expectedState);
    }

    [Test]
    public void Act_AllowOutOfHouseUse()
    {
      var sut = Setup(true, House.Dis, false, out var state, true);

      var expectedState = Expected(House.Dis);

      Act(sut, state, expectedState);
    }

    [Test]
    public void Act_CreatureNotFromActiveHouse_CreatureNotFromActiveHouseException()
    {
      var sut = Setup(true, House.Dis, false, out var state);

      System.Action<NotFromActiveHouseException> asserts = e =>
      {
        Assert.AreEqual(_creature.Card, e.Card);
        Assert.AreEqual(House.Dis, e.House);
      };

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_CreatureStunned_CreatureStunnedException()
    {
      var sut = Setup(true, ActiveHouse, true, out var state);

      System.Action<CreatureStunnedException> asserts = e => Assert.AreEqual(_creature, e.Creature);

      ActExpectException(sut, state, asserts);
    }

    Reap Setup(bool ready, House activeHouse, bool stunned, out MutableState state, bool allowOutOfHouseUse = false)
    {
      _creature = new Creature(_creatureCard, isReady: ready, state: stunned ? CreatureState.Stunned : CreatureState.None);
      _fields = new Dictionary<Player, IMutableList<Creature>>
      {
        {Player.Player1, new LazyList<Creature> {_creature}},
        {Player.Player2, new LazyList<Creature>()}
      }.ToImmutableDictionary();
      state = StateTestUtil.EmptyState.New(activeHouse: activeHouse, fields: _fields);
      return new Reap(null, _creature, allowOutOfHouseUse);
    }

    MutableState Expected(House activeHouse = ActiveHouse)
    {
      var expectedEffects = new LazyStackQueue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.Reap(_creature));
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, fields: _fields, effects: expectedEffects);
      return expectedState;
    }
  }
}