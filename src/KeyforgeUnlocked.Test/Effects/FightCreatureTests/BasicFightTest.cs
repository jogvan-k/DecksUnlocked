using KeyforgeUnlocked.Creatures;
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
    bool _fightingCreatureAfterKillAbilityResolved;
    bool _targetCreatureFightAbilityResolved;
    bool _targetCreatureDestroyedAbilityResolved;
    bool _targetCreatureAfterKillAbilityResolved;

    Callback _fightingCreatureFightAbility;
    Callback _fightingCreatureDestroyedAbility;
    Callback _fightingCreatureAfterKillAbility;
    Callback _targetCreatureFightAbility;
    Callback _targetCreatureDestroyedAbility;
    Callback _targetCreatureAfterKillAbility;

    static readonly Keyword[] Elusive = {Keyword.Elusive};
    static readonly Keyword[] Skirmish = {Keyword.Skirmish};

    [SetUp]
    public void SetUp()
    {
      _fightingCreatureFightAbilityResolved = false;
      _fightingCreatureDestroyedAbilityResolved = false;
      _fightingCreatureAfterKillAbilityResolved = false;
      _targetCreatureFightAbilityResolved = false;
      _targetCreatureDestroyedAbilityResolved = false;
      _targetCreatureAfterKillAbilityResolved = false;
      _fightingCreatureFightAbility = (s, id) => _fightingCreatureFightAbilityResolved = true;
      _fightingCreatureDestroyedAbility = (s, id) => _fightingCreatureDestroyedAbilityResolved = true;
      _fightingCreatureAfterKillAbility = (s, id) => _fightingCreatureAfterKillAbilityResolved = true;
      _targetCreatureFightAbility = (s, id) => _targetCreatureFightAbilityResolved = true;
      _targetCreatureDestroyedAbility = (s, id) => _targetCreatureDestroyedAbilityResolved = true;
      _targetCreatureAfterKillAbility = (s, id) => _targetCreatureAfterKillAbilityResolved = true;
    }

    [Test]
    public void Resolve_FightingCreatureDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(2);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);

      Assert(expectedState, state, true, false);
    }

    [Test]
    public void Resolve_TargetCreatureDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(2);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);
      Assert(expectedState, state, false, true);
    }

    [Test]
    public void Resolve_BothCreaturesDies()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);
      Assert(expectedState, state, true, true);
    }

    [Test]
    public void Resolve_AttackerHasAssault()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(2, 0, Skirmish);
      var targetCreatureCard = InstantiateTargetCreatureCard(3, 0, Skirmish);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFigthingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, isReady: true);
      var expectedState = ExpectedState(expectedFigthingCreature, expectedTargetCreature);
      Assert(expectedState, state, false, false);
    }

    [Test]
    public void Resolve_TargetIsElusive_NoDamageDealt()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(2, 0, Elusive);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard);
      var expectedTargetCreature = new Creature(targetCreatureCard, isReady: true);

      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);
      Assert(expectedState, state, false, false);
    }

    [Test]
    public void Resolve_CreaturesHaveArmor()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3, 1);
      var targetCreatureCard = InstantiateFightingCreatureCard(3, 4);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2, brokenArmor: 1);
      var expectedTargetCreature = new Creature(targetCreatureCard, brokenArmor: 3, isReady: true);

      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);
      Assert(expectedState, state, false, false);
    }

    [Test]
    public void Resolve_CreaturesHaveBrokenArmor()
    {
      var fightingCreatureCard = InstantiateFightingCreatureCard(3, 1);
      var targetCreatureCard = InstantiateTargetCreatureCard(3, 4);

      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard, 1, 3);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 3, brokenArmor: 1);
      var expectedTargetCreature = new Creature(targetCreatureCard, damage: 2, brokenArmor: 4, isReady: true);

      var expectedState = ExpectedState(expectedFightingCreature, expectedTargetCreature);
      Assert(expectedState, state, true, false);
    }

    SampleCreatureCard InstantiateFightingCreatureCard(int power, int armor = 0, Keyword[] keywords = null)
    {
      return new SampleCreatureCard(
        power: power, armor: armor, fightAbility: _fightingCreatureFightAbility,
        afterKillAbility: _fightingCreatureAfterKillAbility,
        destroyedAbility: _fightingCreatureDestroyedAbility, keywords: keywords);
    }

    SampleCreatureCard InstantiateTargetCreatureCard(int power, int armor = 0, Keyword[] keywords = null)
    {
      return new SampleCreatureCard(
        power: power, armor: armor, fightAbility: _targetCreatureFightAbility,
        afterKillAbility: _targetCreatureAfterKillAbility,
        destroyedAbility: _targetCreatureDestroyedAbility, keywords: keywords);
    }

    void Assert(IState expectedState, IState actualState, bool expectedFighterDead,
      bool expectedTargetDead)
    {
      NUnit.Framework.Assert.AreEqual(!expectedFighterDead, _fightingCreatureFightAbilityResolved);
      NUnit.Framework.Assert.False(_targetCreatureFightAbilityResolved);
      NUnit.Framework.Assert.AreEqual(expectedFighterDead, _fightingCreatureDestroyedAbilityResolved);
      NUnit.Framework.Assert.AreEqual(expectedTargetDead, _targetCreatureDestroyedAbilityResolved);
      NUnit.Framework.Assert.AreEqual(!expectedFighterDead && expectedTargetDead, _fightingCreatureAfterKillAbilityResolved);
      NUnit.Framework.Assert.AreEqual(expectedFighterDead && !expectedTargetDead, _targetCreatureAfterKillAbilityResolved);
      StateAsserter.StateEquals(expectedState, actualState);
    }
  }
}