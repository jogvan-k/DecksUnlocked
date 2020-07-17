using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  class DeclareHouseGroupTest
  {
    [Test]
    public void Actions_NoHouses_NoActions()
    {
      var sut = new DeclareHouseGroup(Enumerable.Empty<House>());

      var result = sut.Actions;

      var expectedActions = Enumerable.Empty<Action>();
      Assert.AreEqual(expectedActions, result);
    }

    [Test]
    public void Actions_SampleHouses()
    {
      var sampleHouses = new[] {House.Saurian, House.StarAlliance, House.Untamed};
      var sut = new DeclareHouseGroup(sampleHouses);

      var actions = sut.Actions;

      var expectedActions = new[]
          {new DeclareHouse(sampleHouses[0]), new DeclareHouse(sampleHouses[1]), new DeclareHouse(sampleHouses[2])}
        .ToImmutableHashSet();
      Assert.AreEqual(expectedActions, actions);
    }
  }
}