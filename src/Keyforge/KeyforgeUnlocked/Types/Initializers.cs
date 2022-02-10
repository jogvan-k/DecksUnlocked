using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public static class Initializers
  {
    public static Lookup<Player, int> EmptyValues()
    {
      return new(new Dictionary<Player, int>
      {
        {Player.Player1, 0}, {Player.Player2, 0}
      });
    }

    public static ImmutableLookup<Player, IImmutableList<ICard>> EmptyDeck() =>
      new(new Dictionary<Player, IImmutableList<ICard>>
      {
        { Player.Player1, ImmutableList<ICard>.Empty }, { Player.Player2, ImmutableList<ICard>.Empty }
      });

    public static ImmutableLookup<Player, IImmutableSet<House>> EmptyHouses() =>
      new(new Dictionary<Player, IImmutableSet<House>>
      {
        { Player.Player1, ImmutableHashSet<House>.Empty }, { Player.Player2, ImmutableHashSet<House>.Empty }
      });
    public static ImmutableLookup<Player, IMutableSet<ICard>> EmptySet()
    {
      return new(new Dictionary<Player, IMutableSet<ICard>>()
      {
        {Player.Player1, new LazySet<ICard>()}, {Player.Player2, new LazySet<ICard>()}
      });
    }

    public static ImmutableLookup<Player, IMutableList<Creature>> EmptyField()
    {
      return new(new Dictionary<Player, IMutableList<Creature>>
      {
        {Player.Player1, new LazyList<Creature>()}, {Player.Player2, new LazyList<Creature>()}
      });
    }

    public static ImmutableLookup<Player, IMutableSet<Artifact>> EmptyArtifacts()
    {
      return new(new Dictionary<Player, IMutableSet<Artifact>>
      {
        {Player.Player1, new LazySet<Artifact>()}, {Player.Player2, new LazySet<Artifact>()}
      });
    }
  }
}