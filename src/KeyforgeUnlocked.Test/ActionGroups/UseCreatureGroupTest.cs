using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using FightCreature = KeyforgeUnlocked.Actions.FightCreature;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class UseCreatureGroupTest
  {
    string _creatureId = "creatureId";
    string _opponentCreatureId1 = "opponentCreatureId1";
    string _opponentCreatureId2 = "opponentCreatureId2";

    [Test]
    public void Actions_CreatureNotReady_NoActions()
    {
      var sut = new UseCreatureGroup(StateTestUtil.EmptyState, CreatureTestUtil.SampleLogosCreature(_creatureId, false));

      var actions = sut.Actions;

      Assert.AreEqual(ImmutableList<Action>.Empty, actions);
    }

    [Test]
    public void Actions_CreatureReady()
    {
      var sampleCreature = CreatureTestUtil.SampleLogosCreature(_creatureId);
      var opponentCreatures = new[]
      {
        CreatureTestUtil.SampleUntamedCreature(_opponentCreatureId1),
        CreatureTestUtil.SampleStarAllianceCreature(_opponentCreatureId2)
      };
      var fields = TestUtil.Lists(new[] {sampleCreature}.AsEnumerable(), opponentCreatures);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new UseCreatureGroup(state, sampleCreature);

      var actions = sut.Actions;

      var expectedActions = ImmutableArray<Action>.Empty.AddRange(
        new[]
        {
          (Action) new FightCreature(_creatureId, _opponentCreatureId1),
          new FightCreature(_creatureId, _opponentCreatureId2),
          new Reap(_creatureId)
        });
      Assert.AreEqual(expectedActions, actions);
    }
  }
}