using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public class MutableHistoricData : HistoricDataBase, IMutableHistoricData, IHistoricData
  {
    public bool ActionPlayedThisTurn { get; set; }
    public int EnemiesDestroyedInAFightThisTurn { get; set; }
    public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; set; }
    public IImmutableSet<ICard> CardsDiscardedThisTurn { get; set; }

    public MutableHistoricData(IHistoricData historicData)
    {
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
      return new (this);
    }
  }
}