using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class ReapTest : ActionTestBase
  {
    readonly CreatureCard _creatureCard = new SampleCreatureCard(house: ActiveHouse);
    Creature _creature;
    Dictionary<Player, IList<Creature>> _fields;
    static readonly House ActiveHouse = House.Logos;

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var sut = Setup(false, ActiveHouse, false, out var state);

      Action<CreatureNotReadyException> asserts = e => Assert.AreEqual(_creature, e.Creature);

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
    public void Act_CreatureNotFromActiveHouse_CreatureNotFromActiveHouseException()
    {
      var sut = Setup(true, House.Dis, false, out var state);

      Action<NotFromActiveHouseException> asserts = e =>
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

      Action<CreatureStunnedException> asserts = e => Assert.AreEqual(_creature, e.Creature);

      ActExpectException(sut, state, asserts);
    }

    Reap Setup(bool ready, House activeHouse, bool stunned, out MutableState state)
    {
      _creature = new Creature(_creatureCard, isReady: ready, state: stunned ? CreatureState.Stunned : CreatureState.None);
      _fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {_creature}},
        {Player.Player2, new List<Creature>()}
      };
      state = StateTestUtil.EmptyState.New(activeHouse: activeHouse, fields: _fields);
      return new Reap(_creature);
    }

    MutableState Expected()
    {
      var expectedEffects = new StackQueue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.Reap(_creature));
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: ActiveHouse, fields: _fields, effects: expectedEffects);
      return expectedState;
    }
  }
}