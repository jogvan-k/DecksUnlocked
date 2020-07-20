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
    readonly string CreatureId = "creatureId";

    [Test]
    public void Resolve_EmptyBoard_CreatureNotFoundException()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new Reap(CreatureId);

      Action<CreatureNotPresentException> asserts = e =>
        Assert.AreEqual(CreatureId, e.CreatureId);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {CreatureTestUtil.SampleLogosCreature(CreatureId, false)}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new Reap(CreatureId);

      Action<CreatureNotReadyException> asserts = e => Assert.AreEqual(
        CreatureTestUtil.SampleLogosCreature(CreatureId, false), e.Creature);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_CreatureReady()
    {
      var activeHouse = House.Logos;
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {CreatureTestUtil.SampleLogosCreature(CreatureId, true)}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(activeHouse: activeHouse, fields: fields);
      var sut = new Reap(CreatureId);

      var expectedEffects = new StackQueue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.Reap(CreatureId));
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, fields: fields, effects: expectedEffects);

      Act(sut, state, expectedState);
    }

    [Test]
    public void Act_CreatureNotFromActiveHouse_Exception()
    {
      var creature = CreatureTestUtil.SampleLogosCreature(CreatureId, true);
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {creature}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Dis, fields: fields);
      var sut = new Reap(CreatureId);

      Action<NotFromActiveHouseException> asserts = e =>
      {
        Assert.AreEqual(creature.Card, e.Card);
        Assert.AreEqual(House.Dis, e.House);
      };

      ActExpectException(sut, state, asserts);
    }
  }
}