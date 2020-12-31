using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public sealed class ImmutableHistoricData : HistoricDataBase, IHistoricData
  {
    public bool ActionPlayedThisTurn { get; }
    public int EnemiesDestroyedInAFightThisTurn { get; }
    public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; }

    public ImmutableHistoricData()
    {
      ActionPlayedThisTurn = false;
      EnemiesDestroyedInAFightThisTurn = 0;
      CreaturesAttackedThisTurn = ImmutableHashSet<IIdentifiable>.Empty;
    }
    
    public ImmutableHistoricData(IMutableHistoricData historicData)
    {
      ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
      EnemiesDestroyedInAFightThisTurn = historicData.EnemiesDestroyedInAFightThisTurn;
      CreaturesAttackedThisTurn = historicData.CreaturesAttackedThisTurn;
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