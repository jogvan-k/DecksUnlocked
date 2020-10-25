using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public sealed class Metadata
  {
    public ImmutableDictionary<Player, Deck> InitialDecks { get; }
    public ImmutableDictionary<Player, IImmutableSet<House>> Houses { get; }
    public int TurnCountLimit { get; }

    public Metadata(ImmutableDictionary<Player, Deck> initialDecks,
      ImmutableDictionary<Player, IImmutableSet<House>> houses,
      int turnCountLimit)
    {
      InitialDecks = initialDecks;
      Houses = houses;
      TurnCountLimit = turnCountLimit;
    }
  }
}