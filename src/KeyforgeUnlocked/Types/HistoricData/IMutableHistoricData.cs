using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public interface IMutableHistoricData
  {
    bool ActionPlayedThisTurn { get; set; }
    int EnemiesDestroyedInAFightThisTurn { get; set; }
    IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; set; }
    
    /// <summary>
    /// Returns a mutable copy of the object.
    /// </summary>
    IMutableHistoricData ToMutable();

    /// <summary>
    /// Returns an immutable copy of the object.
    /// </summary>
    ImmutableHistoricData ToImmutable();
  }
}