using System;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public abstract class HistoricDataBase
  {
    public abstract IMutableHistoricData ToMutable();

    public abstract IImmutableHistoricData ToImmutable();

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (!(obj is IHistoricData)) return false;
      return Equals((IHistoricData) obj);
    }
    
    bool Equals(IHistoricData other)
    {
      var thisState = (IHistoricData) this;
      return thisState.ActionPlayedThisTurn == other.ActionPlayedThisTurn;
    }

    public override int GetHashCode()
    {
      var thisState = (IHistoricData) this;
      return HashCode.Combine(thisState.ActionPlayedThisTurn);
    }
  }
}