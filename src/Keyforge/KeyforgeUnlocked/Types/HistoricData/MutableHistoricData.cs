using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.HistoricData
{
    public class MutableHistoricData : HistoricDataBase, IMutableHistoricData, IHistoricData
    {
        Lookup<Player, int> _numberOfShuffles;

        Lookup<Player, int> IMutableHistoricData.NumberOfShuffles
        {
            get => _numberOfShuffles;
            set => _numberOfShuffles = value;
        }

        ImmutableLookup<Player, int> IHistoricData.NumberOfShuffles => _numberOfShuffles.ToReadOnly();

        public bool ActionPlayedThisTurn { get; set; }
        public int EnemiesDestroyedInAFightThisTurn { get; set; }
        public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; set; }
        public IImmutableSet<ICard> CardsDiscardedThisTurn { get; set; }

        public MutableHistoricData(IHistoricData historicData)
        {
            _numberOfShuffles = historicData.NumberOfShuffles.ToLookup();
            ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
            EnemiesDestroyedInAFightThisTurn = historicData.EnemiesDestroyedInAFightThisTurn;
            CreaturesAttackedThisTurn = historicData.CreaturesAttackedThisTurn;
            CardsDiscardedThisTurn = historicData.CardsDiscardedThisTurn;
        }

        public IMutableHistoricData ToMutable()
        {
            return new MutableHistoricData(ToImmutable());
        }

        public ImmutableHistoricData ToImmutable()
        {
            return new(this);
        }
    }
}