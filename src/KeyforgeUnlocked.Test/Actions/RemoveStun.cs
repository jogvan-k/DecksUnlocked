using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class RemoveStun : ActionTestBase
  {
    Creature creature;
    IReadOnlyDictionary<Player, IMutableList<Creature>> fields;

    [Test]
    public void Resolve()
    {
      var sut = Setup(true, true, out var state);

      var expectedState = Expected();

      Act(sut, state, expectedState);
    }

    [Test]
    public void Resolve_CreatureNotStunned_CreatureNotStunnedException()
    {
      var sut = Setup(false, true, out var state);

      Action<CreatureNotStunnedException> asserts = e => e.Equals(creature);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var sut = Setup(true, false, out var state);

      Action<CreatureNotReadyException> asserts = e => e.Equals(creature);

      ActExpectException(sut, state, asserts);
    }

    KeyforgeUnlocked.Actions.RemoveStun Setup(bool stunned, bool isReady, out MutableState state)
    {
      var sampleCreatureCard = new SampleCreatureCard(house: House.Brobnar);
      creature = new Creature(
        sampleCreatureCard, isReady: isReady, state: stunned ? CreatureState.Stunned : CreatureState.None);
      fields = TestUtil.Lists(creature);
      state = StateTestUtil.EmptyState.New(activeHouse: House.Brobnar, fields: fields);
      return new KeyforgeUnlocked.Actions.RemoveStun(null, creature);
    }

    MutableState Expected()
    {
      var expectedEffects = new LazyStackQueue<IEffect>(new[] {new KeyforgeUnlocked.Effects.RemoveStun(creature)});
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: House.Brobnar, fields: fields, effects: expectedEffects);
      return expectedState;
    }
  }
}