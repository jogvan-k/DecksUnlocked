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
      Callback creatureAbility = (s, id) => abilityInvoked = true;
      var creatureCard = new SampleCreatureCard(creatureAbility: creatureAbility);
      var creature = new Creature(creatureCard, isReady: true);
      var fields = TestUtil.Lists(creature);
      var sut = new CreatureAbility(creature);
      var state = StateTestUtil.EmptyMutableState.New(fields: fields);

      sut.Resolve(state);

      var expectedFields = TestUtil.Lists(new Creature(creatureCard));
      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(abilityInvoked);
    }
  }
}