using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
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
    readonly CreatureCard _creatureCard = new SampleCreatureCard(house: House.Logos);

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var creature = new Creature(_creatureCard);
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {creature}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Logos, fields: fields);
      var sut = new Reap(creature);

      Action<CreatureNotReadyException> asserts = e => Assert.AreEqual(creature, e.Creature);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_CreatureReady()
    {
      var activeHouse = House.Logos;
      var creature = new Creature(_creatureCard, isReady: true);
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {creature}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(activeHouse: activeHouse, fields: fields);
      var sut = new Reap(creature);

      var expectedEffects = new StackQueue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.Reap(creature));
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, fields: fields, effects: expectedEffects);

      Act(sut, state, expectedState);
    }

    [Test]
    public void Act_CreatureNotFromActiveHouse_Exception()
    {
      var creature = new Creature(_creatureCard, isReady: true);
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {creature}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Dis, fields: fields);
      var sut = new Reap(creature);

      Action<NotFromActiveHouseException> asserts = e =>
      {
        Assert.AreEqual(creature.Card, e.Card);
        Assert.AreEqual(House.Dis, e.House);
      };

      ActExpectException(sut, state, asserts);
    }
  }
}