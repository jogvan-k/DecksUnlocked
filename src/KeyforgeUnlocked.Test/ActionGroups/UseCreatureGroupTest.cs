using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class UseCreatureGroupTest
  {
    string _creatureId;

    [Test]
    public void Actions_CreatureNotReady_NoActions()
    {
      _creatureId = "creatureId";
      var sut = new UseCreatureGroup(CreatureTestUtil.SampleCreature(_creatureId, false));

      var actions = sut.Actions;

      Assert.AreEqual(ImmutableList<Action>.Empty, actions);
    }

    [Test]
    public void Actions_CreatureReady()
    {
      var sampleCreature = CreatureTestUtil.SampleCreature(_creatureId, true);
      var sut = new UseCreatureGroup(sampleCreature);

      var actions = sut.Actions;

      var expectedActions = ImmutableArray<Action>.Empty.Add(new Reap(_creatureId));
      Assert.AreEqual(expectedActions, actions);
    }
  }
}