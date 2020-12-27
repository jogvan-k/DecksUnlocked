namespace KeyforgeUnlocked.Types.HistoricData
{
  public sealed class ImmutableHistoricData : HistoricDataBase, IImmutableHistoricData
  {
    public bool ActionPlayedThisTurn { get; }

    public ImmutableHistoricData()
    {
      ActionPlayedThisTurn = false;
    }
    
    public ImmutableHistoricData(IHistoricData historicData)
    {
      ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
    }

    public override IMutableHistoricData ToMutable()
    {
      return new LazyHistoricData(this);
    }

    public override IImmutableHistoricData ToImmutable()
    {
      return this;
    }
  }
}