using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class CreatureAbilityTest
  {
    [Test]
    public void Invoke()
    {
      var abilityInvoked = false;
      Delegates.Callback creatureAbility = (s, id) => abilityInvoked = true;
      var creatureCard = new SampleCreatureCard(creatureAbility: creatureAbility);
      var sut = new CreatureAbility(new Creature(creatureCard));
      var state = StateTestUtil.EmptyMutableState;

      sut.Resolve(state);

      StateAsserter.StateEquals(StateTestUtil.EmptyState, state);
      Assert.True(abilityInvoked);
    }
  }
}