using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public class MutableHistoricData : HistoricDataBase, IMutableHistoricData, IHistoricData
  {
    public bool ActionPlayedThisTurn { get; set; }
    public int EnemiesDestroyedInAFightThisTurn { get; set; }
    public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; set; }

    public MutableHistoricData(IHistoricData historicData)
    {
      ActionPlayedThisTurn = historicData.ActionPlayedThisTurn;
      EnemiesDestroyedInAFightThisTurn = historicData.EnemiesDestroyedInAFightThisTurn;
      CreaturesAttackedThisTurn = historicData.CreaturesAttackedThisTurn;
    }

    public IMutableHistoricData ToMutable()
    {
      return new MutableHistoricData(ToImmutable());
    }

    public ImmutableHistoricData ToImmutable()
    {
      return new (this);
    }
  }
}