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
    bool _targetCreatureFightAbilityResolved;

    Delegates.Callback _fightingCreatureFightAbility;
    Delegates.Callback _targetCreatureFightAbility;

    static readonly Keyword[] Elusive = {Keyword.Elusive};
    static readonly Keyword[] Skirmish = {Keyword.Skirmish};

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
      var fightingCreatureCard = new SampleCreatureCard(power: 2, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _targetCreatureFightAbility);

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
      var fightingCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(power: 2, fightAbility: _targetCreatureFightAbility);

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
      var fightingCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _targetCreatureFightAbility);

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
      var fightingCreatureCard = new SampleCreatureCard(
        power: 2, fightAbility: _fightingCreatureFightAbility, keywords: Skirmish);
      var targetCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _targetCreatureFightAbility, keywords: Skirmish);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFigthingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFigthingCreature, expectedTargetCreature);
      var expectedState = ExpectedState(expectedFigthingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true);
    }

    [Test]
    public void Resolve_TargetIsElusive_NoDamageDealt()
    {
      var fightingCreatureCard = new SampleCreatureCard(power: 3, fightAbility: _fightingCreatureFightAbility);
      var targetCreatureCard = new SampleCreatureCard(power: 2, fightAbility: _targetCreatureFightAbility, keywords: Elusive);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, isReady: true);
      var targetFoughtResolvedEffect = new CreatureFought(expectedFightingCreature, expectedTargetCreature);

      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature, targetFoughtResolvedEffect);
      Assert(expectedState, state, true);
    }

    void Assert(IState expectedState, IState actualState, bool expectFighterAbilityTriggered)
    {
      NUnit.Framework.Assert.AreEqual(expectFighterAbilityTriggered, _fightingCreatureFightAbilityResolved);
      NUnit.Framework.Assert.False(_targetCreatureFightAbilityResolved);
      StateAsserter.StateEquals(expectedState, actualState);
    }
  }
}