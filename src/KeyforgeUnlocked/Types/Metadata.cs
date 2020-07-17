using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Types
{
  public sealed class Metadata
  {
    public ImmutableDictionary<Player, Deck> InitialDecks { get; }

    public ImmutableDictionary<Player, IImmutableSet<House>> Houses { get; }

    public Metadata(ImmutableDictionary<Player, Deck> initialDecks,
      ImmutableDictionary<Player, IImmutableSet<House>> houses)
    {
      InitialDecks = initialDecks;
      Houses = houses;
    }
  }
}