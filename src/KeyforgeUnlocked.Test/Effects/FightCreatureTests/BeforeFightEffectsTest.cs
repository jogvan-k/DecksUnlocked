using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
  [TestFixture]
  sealed class BeforeFightEffectsTest : FightCreatureTestBase
  {
    [Test]
    public void TargetCreatureDies_NoFight()
    {
      _fightingCreatureBeforeFightAbility = (s, _, _) =>
      {
        _fightingCreatureBeforeFightAbilityResolved = true;
        s.DamageCreature(_targetCreature, 5);
      };
      
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);


      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedTarget = new Creature(targetCreatureCard, damage: 5, isReady: true);
      var expectedState = ExpectedState(new Creature(fightingCreatureCard), expectedTarget, false);
      expectedState.ResolvedEffects.Insert(0, new CreatureDamaged(expectedTarget, 5));
      Assert(expectedState, state, false, true, false);
    }
    
    [Test]
    public void FightingCreatureDies_NoFight()
    {
      _fightingCreatureBeforeFightAbility = (s, t, _) =>
      {
        _fightingCreatureBeforeFightAbilityResolved = true;
        s.DamageCreature(t, 5);
      };
      
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(3);


      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFighter = new Creature(fightingCreatureCard, damage: 5);
      var expectedState = ExpectedState(expectedFighter, new Creature(targetCreatureCard, isReady: true), false);
      expectedState.ResolvedEffects.Insert(0, new CreatureDamaged(expectedFighter, 5));
      Assert(expectedState, state, true, false, false);
    }

    [Test]
    public void TargetDamaged_FightOccurs()
    { 
      _fightingCreatureBeforeFightAbility = (s, _, _) =>
      {
        _fightingCreatureBeforeFightAbilityResolved = true;
        s.DamageCreature(_targetCreature, 2);
      };
      
      var fightingCreatureCard = InstantiateFightingCreatureCard(3);
      var targetCreatureCard = InstantiateTargetCreatureCard(5);


      var state = SetupAndAct(fightingCreatureCard, targetCreatureCard);

      var expectedFighter = new Creature(fightingCreatureCard, damage: 5);
      var expectedTarget = new Creature(targetCreatureCard, damage: 5, isReady: true);
      var expectedState = ExpectedState(expectedFighter, expectedTarget, true);
      expectedState.ResolvedEffects.Insert(0, new CreatureDamaged(new Creature(targetCreatureCard, damage: 2, isReady:true), 2));
      Assert(expectedState, state, true, true, true);
    }
  }
}