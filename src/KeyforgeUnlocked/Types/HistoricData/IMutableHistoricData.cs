namespace KeyforgeUnlocked.Types.HistoricData
{
  public interface IMutableHistoricData : IHistoricData
  {
    bool ActionPlayedThisTurn { get; set; }
  }
}