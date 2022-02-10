using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public sealed class Metadata
  {
    public ImmutableLookup<Player, IImmutableList<ICard>> InitialDecks { get; }
    public ImmutableLookup<Player, IImmutableSet<House>> Houses { get; }
    public int TurnCountLimit { get; }
    public int RngSeed { get; }

    public Metadata(ImmutableLookup<Player, IImmutableList<ICard>> initialDecks,
      ImmutableLookup<Player, IImmutableSet<House>> houses,
      int turnCountLimit,
      int rngSeed)
    {
      InitialDecks = initialDecks;
      Houses = houses;
      TurnCountLimit = turnCountLimit;
      RngSeed = rngSeed;
    }

    bool Equals(Metadata other)
    {
      return EqualityComparer.Equals(InitialDecks, other.InitialDecks)
             && EqualityComparer.Equals(Houses, other.Houses)
             && TurnCountLimit == other.TurnCountLimit
             && RngSeed == other.RngSeed;
    }

    public override bool Equals(object? obj)
    {
      return ReferenceEquals(this, obj) || obj is Metadata other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(InitialDecks, Houses, TurnCountLimit, RngSeed);
    }
  }
}