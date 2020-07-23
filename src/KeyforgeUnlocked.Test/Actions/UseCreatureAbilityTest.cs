using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class UseCreatureAbilityTest : ActionTestBase
  {
    [Test]
    public void Resolve()
    {
      var sampleCreatureCard = new SampleCreatureCard(house: House.Shadows);
      var sampleCreature = new Creature(sampleCreatureCard, isReady: true);
      var fields = TestUtil.Lists(sampleCreature);
      var state = StateTestUtil.EmptyState.New(activeHouse: House.Shadows, fields: fields);
      var sut = new UseCreatureAbility(sampleCreature);

      var expectedEffects = new StackQueue<IEffect>(new[] {new CreatureAbility(sampleCreature)});
      var expectedState = StateTestUtil.EmptyState.New(activeHouse: House.Shadows, fields: fields, effects: expectedEffects);

      Act(sut, state, expectedState);
    }
  }
}