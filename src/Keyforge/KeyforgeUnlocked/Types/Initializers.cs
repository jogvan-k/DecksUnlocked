using System.Collections.Generic;
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