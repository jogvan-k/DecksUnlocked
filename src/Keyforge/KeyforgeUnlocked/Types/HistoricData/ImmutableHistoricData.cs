using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.HistoricData
{
    public sealed class ImmutableHistoricData : HistoricDataBase, IHistoricData
    {
        public ImmutableLookup<Player, int> NumberOfShuffles { get; }
        public bool ActionPlayedThisTurn { get; }
        public int EnemiesDestroyedInAFightThisTurn { get; }
        public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; }
        public IImmutableSet<ICard> CardsDiscardedThisTurn { get; }

        public ImmutableHistoricData()
        {
            NumberOfShuffles = Initializers.EmptyValues().ToReadOnly();
            ActionPlayedThisTurn = false;
            EnemiesDestroyedInAFightThisTurn = 0;
            CreaturesAttackedThisTurn = ImmutableHashSet<IIdentifiable>.Empty;
            CardsDiscardedThisTurn = ImmutableHashSet<ICard>.Empty;
        }

        public ImmutableHistoricData(IMutableHistoricData historicData)
        {
            NumberOfShuffles = historicData.NumberOfShuffles.ToReadOnly();
            ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
            EnemiesDestroyedInAFightThisTurn = historicData.EnemiesDestroyedInAFightThisTurn;
            CreaturesAttackedThisTurn = historicData.CreaturesAttackedThisTurn;
            CardsDiscardedThisTurn = historicData.CardsDiscardedThisTurn;
        }

        public IMutableHistoricData ToMutable()
        {
            return new LazyHistoricData(this);
        }

        public ImmutableHistoricData ToImmutable()
        {
            return this;
        }
    }
}