namespace KeyforgeUnlocked.Types.HistoricData
{
  public class MutableHistoricData : HistoricDataBase, IMutableHistoricData
  {
    public bool ActionPlayedThisTurn { get; set; }

    public MutableHistoricData(IHistoricData historicData)
    {
      ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
    }

    public override IMutableHistoricData ToMutable()
    {
      return new MutableHistoricData(this);
    }

    public override IImmutableHistoricData ToImmutable()
    {
      return new ImmutableHistoricData(this);
    }
  }
}