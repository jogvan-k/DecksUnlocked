using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public sealed class ImmutableHistoricData : HistoricDataBase, IHistoricData
  {
    public bool ActionPlayedThisTurn { get; }
    public int EnemiesDestroyedInAFightThisTurn { get; }
    public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; }
    public IImmutableSet<ICard> CardsDiscardedThisTurn { get; }
    public ImmutableHistoricData()
    {
      ActionPlayedThisTurn = false;
      EnemiesDestroyedInAFightThisTurn = 0;
      CreaturesAttackedThisTurn = ImmutableHashSet<IIdentifiable>.Empty;
      CardsDiscardedThisTurn = ImmutableHashSet<ICard>.Empty;
    }
    
    public ImmutableHistoricData(IMutableHistoricData historicData)
    {
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