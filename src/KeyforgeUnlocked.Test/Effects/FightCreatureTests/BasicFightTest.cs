using KeyforgeUnlocked.Creatures;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  [TestFixture]
  sealed class BasicFightTest : FightCreatureTestBase
  {
    static readonly Keyword[] Elusive = {Keyword.Elusive};
    static readonly Keyword[] Skirmish = {Keyword.Skirmish};

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
      var targetCreatureCard = InstantiateTargetCreatureCard(3, 4);

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
  }
}