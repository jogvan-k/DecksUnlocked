using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class DeclareHouseGroupTest
  {
    private ImmutableState _state = StateTestUtil.EmptyState;
    
    [Test]
    public void Actions_NoHouses_NoActions()
    {
      var sut = new DeclareHouseGroup(Enumerable.Empty<House>());

      var result = sut.Actions(_state);

      var expectedActions = Enumerable.Empty<Action>();
      Assert.AreEqual(expectedActions, result);
    }

    [Test]
    public void Actions_SampleHouses()
    {
      var sampleHouses = new[] {House.Saurian, House.StarAlliance, House.Untamed};
      var sut = new DeclareHouseGroup(sampleHouses);

      var actions = sut.Actions(_state);

      var expectedActions = new[]
          {new DeclareHouse(_state, sampleHouses[0]), new DeclareHouse(_state, sampleHouses[1]), new DeclareHouse(_state, sampleHouses[2])}
        .ToImmutableList();
      Assert.AreEqual(expectedActions, actions);
    }
  }
}