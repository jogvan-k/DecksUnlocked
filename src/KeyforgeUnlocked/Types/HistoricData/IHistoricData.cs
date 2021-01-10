using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public interface IHistoricData
  {
    ImmutableLookup<Player, int> NumberOfShuffles { get; }
    bool ActionPlayedThisTurn { get; }
    int EnemiesDestroyedInAFightThisTurn { get; }
    IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn { get; }
    IImmutableSet<ICard> CardsDiscardedThisTurn { get; }

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