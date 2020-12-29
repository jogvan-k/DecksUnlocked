namespace KeyforgeUnlocked.Types.HistoricData
{
  public sealed class ImmutableHistoricData : HistoricDataBase, IHistoricData
  {
    public bool ActionPlayedThisTurn { get; }
    public int EnemiesDestroyedInAFightThisTurn { get; }

    public ImmutableHistoricData()
    {
      ActionPlayedThisTurn = false;
      EnemiesDestroyedInAFightThisTurn = 0;
    }
    
    public ImmutableHistoricData(IMutableHistoricData historicData)
    {
      ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
      EnemiesDestroyedInAFightThisTurn = historicData.EnemiesDestroyedInAFightThisTurn;
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