using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
using KeyforgeUnlocked.Types.HistoricData.Extensions;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

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
      historicData.NumberOfShuffles = TestUtil.Ints(2, 3);
      historicData.ActionPlayedThisTurn = true;
      historicData.EnemiesDestroyedInAFightThisTurn = 5;
      historicData.CreaturesAttackedThisTurn = ImmutableHashSet<IIdentifiable>.Empty
        .Add(new Identifiable(new SampleCreatureCard()))
        .Add(new Identifiable(new SampleCreatureCard()));
      historicData.CardsDiscardedThisTurn = ImmutableHashSet<ICard>.Empty
        .Add(new SampleActionCard())
        .Add(new SampleCreatureCard());
      
      historicData.NextTurn();

      var expectedHistoricData = new ImmutableHistoricData().ToMutable();
      expectedHistoricData.NumberOfShuffles = TestUtil.Ints(2, 3);
      Assert.AreEqual(expectedHistoricData, historicData);
    }
  }
}