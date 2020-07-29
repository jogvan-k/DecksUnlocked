using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class ReadyCardsAndRestoreArmorTest
  {
    readonly ReadyCardsAndRestoreArmor _sut = new ReadyCardsAndRestoreArmor();

    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;

      _sut.Resolve(state);

      var expectedState = StateTestUtil.EmptyState;
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_ReadyAndUnreadyCreaturesWithBrokenArmor()
    {
      var playerCreatureCard1 = new SampleCreatureCard(armor: 0);
      var playerCreatureCard2 = new SampleCreatureCard(armor: 1);
      var opponentCreatureCard1 = new SampleCreatureCard(armor: 2);
      var opponentCreatureCard2 = new SampleCreatureCard(armor: 3);
      var fields = TestUtil.Lists(
        new[]
        {
          new Creature(playerCreatureCard1, isReady: true),
          new Creature(playerCreatureCard2, isReady: false, brokenArmor: 1)
        }.AsEnumerable(), new[]
        {
          new Creature(opponentCreatureCard1, isReady: true, brokenArmor: 2),
          new Creature(opponentCreatureCard2, isReady: false, brokenArmor: 2)
        });
      var state = StateTestUtil.EmptyMutableState.New(fields: fields);

      _sut.Resolve(state);

      var expectedFields = TestUtil.Lists(
        new[]
        {
          new Creature(playerCreatureCard1, isReady: true),
          new Creature(playerCreatureCard2, isReady: true)
        }.AsEnumerable(), new[]
        {
          new Creature(opponentCreatureCard1, isReady: true),
          new Creature(opponentCreatureCard2, isReady: false)
        });

      var expectedResolvedEffects = new List<IResolvedEffect> { new CreatureReadied(new Creature(playerCreatureCard2, isReady: true))};
      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}