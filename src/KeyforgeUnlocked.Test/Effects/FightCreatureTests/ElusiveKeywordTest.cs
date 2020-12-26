using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  [TestFixture]
  sealed class ElusiveKeywordTest
  {
    static readonly Keyword[] Elusive = {Keyword.Elusive};

    [Test]
    public void Resolve_TargetIsElusiveAndAttackedPreviousTurn_NoDamageDealt()
    {
      var fightingCreatureCard = new SampleCreatureCard(power: 3);
      var targetCreatureCard = new SampleCreatureCard(power: 2, keywords: Elusive);
      var fightingCreature = new Creature(fightingCreatureCard, isReady: true);
      var targetCreature = new Creature(targetCreatureCard);
      var resolvedEffects = new[]
        {(IResolvedEffect) new CreatureFought(new Creature(fightingCreature.Card), targetCreature)};
      var fields = TestUtil.Lists(fightingCreature, targetCreature);
      var startState = StateTestUtil.EmptyState.New(
          turnNumber: 1,
          fields: fields,
          resolvedEffects: new LazyList<IResolvedEffect>(resolvedEffects))
        .Extend(turnNumber: 2).ToImmutable();
      var state = startState.ToMutable();
      var sut = new FightCreature(fightingCreature, targetCreature);

      sut.Resolve(state);

      var expectedFields = TestUtil.Lists(new Creature(fightingCreatureCard), targetCreature);
      var expectedState = startState.Extend(resolvedEffects: new LazyList<IResolvedEffect>(resolvedEffects), fields: expectedFields);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_TargetIsElusiveAndAttackedCurrentTurn_ElusiveCreatureDead()
    {
      var foughtCreatureCard = new SampleCreatureCard(power: 3);
      var fightingCreatureCard = new SampleCreatureCard(power: 3);
      var targetCreatureCard = new SampleCreatureCard(power: 2, keywords: Elusive);
      var foughtCreature = new Creature(foughtCreatureCard);
      var fightingCreature = new Creature(fightingCreatureCard, isReady: true);
      var targetCreature = new Creature(targetCreatureCard);
      IResolvedEffect[] resolvedEffects = {new CreatureFought(foughtCreature, targetCreature)};
      var fields = TestUtil.Lists(new[] {foughtCreature, fightingCreature}.AsEnumerable(), new[] {targetCreature});
      var startState = StateTestUtil.EmptyState.New(
          turnNumber: 1,
          fields: fields,
          resolvedEffects: new LazyList<IResolvedEffect>(resolvedEffects))
        .Extend().ToImmutable();
      var state = startState.ToMutable();
      var sut = new FightCreature(fightingCreature, targetCreature);

      sut.Resolve(state);

      var expectedField = TestUtil.Lists(new[] {foughtCreature, new Creature(fightingCreatureCard, damage: 2)}, Enumerable.Empty<Creature>());
      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3);
      var expectedResolvedEffects = new List<IResolvedEffect> {new CreatureFought(expectedFightingCreature, expectedTargetCreature), new CreatureDied(expectedTargetCreature)};
      var expectedDiscards = TestUtil.Sets(Enumerable.Empty<ICard>(), new []{targetCreatureCard});
      var expectedState = startState.Extend(resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects), fields: expectedField, discards: expectedDiscards);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}