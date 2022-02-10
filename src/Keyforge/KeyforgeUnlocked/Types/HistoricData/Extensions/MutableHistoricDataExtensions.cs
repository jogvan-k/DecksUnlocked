using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Types.HistoricData.Extensions
{
    public static class MutableHistoricDataExtensions
    {
        public static void NextTurn(this IMutableHistoricData historicData)
        {
            historicData.ActionPlayedThisTurn = false;
            historicData.EnemiesDestroyedInAFightThisTurn = 0;
            historicData.CreaturesAttackedThisTurn = ImmutableHashSet<IIdentifiable>.Empty;
            historicData.CardsDiscardedThisTurn = ImmutableHashSet<ICard>.Empty;
        }
    }
}