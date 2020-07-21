using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class FightCreatureTest
  {
    [Test]
    public void Resolve_TargetCreatureDies()
    {
      var fightingCreatureCard = new SampleCreatureCard(3);
      var targetCreatureCard = new SampleCreatureCard(2);
      var fightingCreature = new Creature(fightingCreatureCard, isReady: true);
      var targetCreature = new Creature(targetCreatureCard, isReady: true);
      var fields = TestUtil.Lists(fightingCreature, targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new FightCreature(fightingCreature, targetCreature);

      sut.Resolve(state);

      var expectedFightingCreature = new Creature(fightingCreatureCard, damage: 2);
      var expectedDeadCreature = new Creature(targetCreatureCard, damage: 3, isReady: true);
      var expectedFields = TestUtil.Lists(new[] {expectedFightingCreature}, Enumerable.Empty<Creature>());
      var expectedDiscards = TestUtil.Sets(Enumerable.Empty<Card>(), new[] {targetCreatureCard});
      var expectedResolvedEffects = new[]
      {
        (IResolvedEffect) new CreatureFought(expectedFightingCreature, expectedDeadCreature),
        new CreatureDied(expectedDeadCreature)
      };
      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}