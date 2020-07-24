using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  abstract class FightCreatureTestBase
  {
    protected MutableState SetupAndAct(
      SampleCreatureCard fightingCreatureCard,
      SampleCreatureCard targetCreatureCard)
    {
      var fightingCreature = new Creature(fightingCreatureCard, isReady: true);
      var targetCreature = new Creature(targetCreatureCard, isReady: true);
      var fields = TestUtil.Lists(fightingCreature, targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new FightCreature(fightingCreature, targetCreature);

      sut.Resolve(state);

      return state;
    }

    protected MutableState ExpectedState(
      Creature expectedFighter,
      Creature expectedTarget,
      params IResolvedEffect[] preResolvedEffects)
    {
      var fighterDead = expectedFighter.Health <= 0;
      var targetDead = expectedTarget.Health <= 0;
      var expectedFields = TestUtil.Lists(
        fighterDead ? Enumerable.Empty<Creature>() : new[] {expectedFighter},
        targetDead ? Enumerable.Empty<Creature>() : new[] {expectedTarget});

      var expectedDiscards = TestUtil.Sets(
        fighterDead ? new[] {expectedFighter.Card} : Enumerable.Empty<Card>(),
        targetDead ? new[] {expectedTarget.Card} : Enumerable.Empty<Card>());

      var resolvedEffects = new List<IResolvedEffect>(preResolvedEffects);
      if (fighterDead)
      {
        resolvedEffects.Add(new CreatureDied(expectedFighter));
      }

      if (targetDead)
      {
        resolvedEffects.Add(new CreatureDied(expectedTarget));
      }

      return StateTestUtil.EmptyState.New(
        fields: expectedFields, discards: expectedDiscards, resolvedEffects: resolvedEffects);
    }
  }
}