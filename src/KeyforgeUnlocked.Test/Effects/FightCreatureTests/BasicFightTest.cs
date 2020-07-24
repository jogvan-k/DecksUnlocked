using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  [TestFixture]
  sealed class BasicFightTest : FightCreatureTestBase
  {
    bool _fightingCreatureFightAbilityResolved;
    bool _fightingCreatureDestroyedAbilityResolved;
    bool _targetCreatureFightAbilityResolved;
    bool _targetCreatureDestroyedAbilityResolved;

    Callback _fightingCreatureFightAbility;
    Callback _fightingCreatureDestroyedAbility;
    Callback _targetCreatureFightAbility;
    Callback _targetCreatureDestroyedAbility;

    static readonly Keyword[] Elusive = {Keyword.Elusive};
    static readonly Keyword[] Skirmish = {Keyword.Skirmish};

    [SetUp]
    public void SetUp()
    {
      _fightingCreatureFightAbilityResolved = false;
      _fightingCreatureDestroyedAbilityResolved = false;
      _targetCreatureFightAbilityResolved = false;
      _targetCreatureDestroyedAbilityResolved = false;
      _fightingCreatureFightAbility = (s, id) => _fightingCreatureFightAbilityResolved = true;
      _fightingCreatureDestroyedAbility = (s, id) => _fightingCreatureDestroyedAbilityResolved = true;
      _targetCreatureFightAbility = (s, id) => _targetCreatureFightAbilityResolved = true;
      _targetCreatureDestroyedAbility = (s, id) => _targetCreatureDestroyedAbilityResolved = true;
    }

    [Test]
    public void Resolve_FightingCreatureDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(2);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);

      Assert(expectedState, state, false, true, false);
    }

    [Test]
    public void Resolve_TargetCreatureDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(2);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true, false, true);
    }

    [Test]
    public void Resolve_BothCreaturesDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, false, true, true);
    }

    [Test]
    public void Resolve_AttackerHasAssault()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(2, Skirmish);
      var targetCreatureCard = InstantiateTargetCreatureCard(3, Skirmish);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFigthingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFigthingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFigthingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true, false, false);
    }

    [Test]
    public void Resolve_TargetIsElusive_NoDamageDealt()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(2, Elusive);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);

      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true, false, false);
    }

    SampleCreatureCard InstantiateFightingCreatureCard(int power, Keyword[] keywords = null)
    {
      return new SampleCreatureCard(power: power, fightAbility: _fightingCreatureFightAbility, destroyedAbility: _fightingCreatureDestroyedAbility, keywords: keywords);
    }

    SampleCreatureCard InstantiateTargetCreatureCard(int power, Keyword[] keywords = null)
    {
      return new SampleCreatureCard(power: power, fightAbility: _targetCreatureFightAbility, destroyedAbility: _targetCreatureDestroyedAbility, keywords: keywords);
    }

    void Assert(IState expectedState, IState actualState, bool expectFighterAbilityTriggered, bool expectedFighterDead, bool expectedTargetDead)
    {
      NUnit.Framework.Assert.AreEqual(expectFighterAbilityTriggered, _fightingCreatureFightAbilityResolved);
      NUnit.Framework.Assert.False(_targetCreatureFightAbilityResolved);
      NUnit.Framework.Assert.AreEqual(expectedFighterDead, _fightingCreatureDestroyedAbilityResolved);
      NUnit.Framework.Assert.AreEqual(expectedTargetDead, _targetCreatureDestroyedAbilityResolved);
      StateAsserter.StateEquals(expectedState, actualState);
    }
  }
}