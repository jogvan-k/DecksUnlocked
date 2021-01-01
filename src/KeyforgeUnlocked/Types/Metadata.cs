using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public sealed class Metadata
  {
    public ImmutableDictionary<Player, IImmutableList<ICard>> InitialDecks { get; }
    public ImmutableDictionary<Player, IImmutableSet<House>> Houses { get; }
    public int TurnCountLimit { get; }

    public Metadata(ImmutableDictionary<Player, Deck> initialDecks,
      ImmutableDictionary<Player, IImmutableSet<House>> houses,
      int turnCountLimit)
    {
      InitialDecks = initialDecks.ToImmutableDictionary(kv => kv.Key, kv => (IImmutableList<ICard>)kv.Value.Cards);
      Houses = houses;
      TurnCountLimit = turnCountLimit;
    }
  }
}