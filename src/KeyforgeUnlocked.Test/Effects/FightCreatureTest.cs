using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class FightCreatureTest
  {
    bool _fightingCreatureFightAbilityResolved;
    bool _targetCreatureFightAbilityResolved;

    Delegates.Callback _fightingCreatureFightAbility;
    Delegates.Callback _targetCreatureFightAbility;

    [SetUp]
    public void SetUp()
    {
      _fightingCreatureFightAbilityResolved = false;
      _targetCreatureFightAbilityResolved = false;
      _fightingCreatureFightAbility = s => _fightingCreatureFightAbilityResolved = true;
      _targetCreatureFightAbility = s => _targetCreatureFightAbilityResolved = true;
    }

    [Test]
    public void Resolve_FightingCreatureDies()
    {
      var fightingCreatureCard = new SampleCreatureCard(2, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(3, fightAbility: _targetCreatureFightAbility);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);

      Assert(expectedState, state, false);
    }

    [Test]
    public void Resolve_TargetCreatureDies()
    {
      var fightingCreatureCard = new SampleCreatureCard(3, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(2, fightAbility: _targetCreatureFightAbility);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true);
    }

    [Test]
    public void Resolve_BothCreaturesDies()
    {
      var fightingCreatureCard = new SampleCreatureCard(3, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(3, fightAbility: _targetCreatureFightAbility);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, false);
    }

    [Test]
    public void Resolve_AttackerHasAssault()
    {
      var fightingCreatureCard = new SampleCreatureCard(2, keywords: new[] {Keyword.Skirmish}, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(3, keywords: new[] {Keyword.Skirmish}, fightAbility: _targetCreatureFightAbility);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFigthingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFigthingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFigthingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true);
    }

    static MutableState SetupAndAct(
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

    static MutableState ExpectedState(
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

    void Assert(IState expectedState, IState actualState, bool expectFighterAbilityTriggered)
    {
      NUnit.Framework.Assert.AreEqual(expectFighterAbilityTriggered, _fightingCreatureFightAbilityResolved);
      NUnit.Framework.Assert.False(_targetCreatureFightAbilityResolved);
      StateAsserter.StateEquals(expectedState, actualState);
    }
  }
}