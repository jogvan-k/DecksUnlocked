using System;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public abstract class HistoricDataBase
  {
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj is IHistoricData historicData) return Equals(historicData);
      return false;
    }

    bool Equals(IHistoricData other)
    {
      var thisState = (IHistoricData) this;
      return thisState.ActionPlayedThisTurn == other.ActionPlayedThisTurn
             && thisState.EnemiesDestroyedInAFightThisTurn == other.EnemiesDestroyedInAFightThisTurn
             && thisState.CreaturesAttackedThisTurn.SetEquals(other.CreaturesAttackedThisTurn);
    }

    public override int GetHashCode()
    {
      var thisState = (IHistoricData) this;
      return HashCode.Combine(
        thisState.ActionPlayedThisTurn,
        thisState.EnemiesDestroyedInAFightThisTurn,
        EqualityComparer.GetHashCode(thisState.CreaturesAttackedThisTurn));
    }
  }
}