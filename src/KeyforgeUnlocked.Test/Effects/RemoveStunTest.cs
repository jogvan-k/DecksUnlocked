using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class RemoveStunTest
  {
    [Test]
    public void Invoke()
    {
      var sampleCreatureCard = new SampleCreatureCard();
      var creature = new Creature(sampleCreatureCard, state: CreatureState.Stunned, isReady: true);
      var state = StateTestUtil.EmptyState.New(fields: TestUtil.Lists(creature));
      var sut = new RemoveStun(creature);

      sut.Resolve(state);

      var expectedCreature = new Creature(sampleCreatureCard);
      var expectedFields = TestUtil.Lists(expectedCreature);
      var expectedResolvedEffects = new List<IResolvedEffect>{new StunRemoved(expectedCreature)};
      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}