using KeyforgeUnlocked.Types.HistoricData;
using KeyforgeUnlocked.Types.HistoricData.Extensions;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Types
{
  [TestFixture]
  sealed class MutableHistoricDataExtensions
  {
    [Test]
    public void NextTurn_DefaultHistoricData()
    {
      var historicData = new ImmutableHistoricData().ToMutable();
      
      historicData.NextTurn();

      var expectedHistoricData = new ImmutableHistoricData();
      Assert.AreEqual(expectedHistoricData, historicData);
    }

    [Test]
    public void NextTurn()
    {
      var historicData = new ImmutableHistoricData().ToMutable();
      historicData.ActionPlayedThisTurn = true;
      historicData.EnemiesDestroyedInAFightThisTurn = 5;
      
      historicData.NextTurn();

      var expectedHistoricData = new ImmutableHistoricData();
      Assert.AreEqual(expectedHistoricData, historicData);
    }
  }
}