using System.Linq;
using System.Reflection;
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
    CreatureCard fightingCreatureCard = new SampleCreatureCard();
    CreatureCard targetCreatureCard = new SampleCreatureCard();

    [Test]
    public void Resolve_TargetCreatureDies()
    {
      var fightingCreature = new Creature(3, 0, fightingCreatureCard);
      var targetCreature = new Creature(2, 0, targetCreatureCard);
      var fields = TestUtil.Lists(fightingCreature, targetCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new FightCreature(fightingCreature, targetCreature);

      sut.Resolve(state);

      var expectedFightingCreature = new Creature(
        fightingCreature.Id,
        3, 0, fightingCreatureCard,
        0, 2, false);
      var expectedDeadCreature = new Creature(
        targetCreature.Id, 2, 0,
        targetCreatureCard, 0, 3,
        true);
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