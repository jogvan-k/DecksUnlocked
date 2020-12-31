using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
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
  sealed class UseCreatureAbilityTest : ActionTestBase<UseCreatureAbility>
  {
    Creature creature;
    IReadOnlyDictionary<Player, IMutableList<Creature>> fields;

    [Test]
    public void Resolve()
    {
      var sut = Setup(true, false, out var state);

      var expectedState = Expected();

      ActAndAssert(sut, state, expectedState);
    }

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var sut = Setup(false, false, out var state);

      System.Action<CreatureNotReadyException> asserts = e => e.Creature.Equals(creature);

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Resolve_CreatureStunned_CreatureStunnedException()
    {
      var sut = Setup(true, true, out var state);

      System.Action<CreatureStunnedException> asserts = e => e.Creature.Equals(creature);

      ActExpectException(sut, state, asserts);
    }

    UseCreatureAbility Setup(bool ready, bool stunned, out IMutableState state)
    {
      var sampleCreatureCard = new SampleCreatureCard(house: House.Shadows);
      creature = new Creature(
        sampleCreatureCard, isReady: ready, state: stunned ? CreatureState.Stunned : CreatureState.None);
      fields = TestUtil.Lists(creature);
      state = StateTestUtil.EmptyState.New(activeHouse: House.Shadows, fields: fields);
      return new UseCreatureAbility(null, creature);
    }

    IMutableState Expected()
    {
      var expectedEffects = new LazyStackQueue<IEffect>(new[] {new CreatureAbility(creature)});
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: House.Shadows, fields: fields, effects: expectedEffects);
      expectedState.HistoricData.ActionPlayedThisTurn = true;
      return expectedState;
    }
  }
}