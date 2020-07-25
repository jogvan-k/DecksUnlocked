using KeyforgeUnlocked.Creatures;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  [TestFixture]
  sealed class PoisonKeywordTest : FightCreatureTestBase
  {
    static Keyword[] Poison = {Keyword.Poison};

    [Test]
    public void Resolve_AttackerHasPoison()
    {
      var attacker = new SampleCreatureCard(power: 1, keywords: Poison);
      var target = new SampleCreatureCard(power: 3);
      var state = SetupAndAct(attacker, target);

      var expectedAttacker = new Creature(attacker, damage: 3);
      var expectedTarget = new Creature(target, damage: 1001, isReady: true);
      var expectedState = ExpectedState(expectedAttacker, expectedTarget);

      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_TargetHasPoison()
    {
      var attacker = new SampleCreatureCard(power: 3);
      var target = new SampleCreatureCard(power: 1, keywords: Poison);
      var state = SetupAndAct(attacker, target);

      var expectedAttacker = new Creature(attacker, damage: 1001);
      var expectedTarget = new Creature(target, damage: 3, isReady: true);
      var expectedState = ExpectedState(expectedAttacker, expectedTarget);

      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_AttackerHasPoisonTargetHasSameArmorAsAttackersPower()
    {
      var attacker = new SampleCreatureCard(power: 1, keywords: Poison);
      var target = new SampleCreatureCard(power: 1, armor: 1);
      var state = SetupAndAct(attacker, target);

      var expectedAttacker = new Creature(attacker, damage: 1);
      var expectedTarget = new Creature(target, brokenArmor: 1, isReady: true);
      var expectedState = ExpectedState(expectedAttacker, expectedTarget);

      StateAsserter.StateEquals(expectedState, state);
    }
  }
}