namespace KeyforgeUnlocked.Types.HistoricData.Extensions
{
  public static class MutableHistoricDataExtensions
  {
    public static void NextTurn(this IMutableHistoricData historicData)
    {
      historicData.ActionPlayedThisTurn = false;
    } 
  }
}