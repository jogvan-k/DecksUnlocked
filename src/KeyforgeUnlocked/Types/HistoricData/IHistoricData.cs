﻿namespace KeyforgeUnlocked.Types.HistoricData
{
  public interface IHistoricData
  {
    bool ActionPlayedThisTurn { get; }
    int EnemiesDestroyedInAFightThisTurn { get; }
    
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