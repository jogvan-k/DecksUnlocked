using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithPlayer<T> : Equatable<T>, IResolvedEffect where T : ResolvedEffectWithPlayer<T>
  {
    protected readonly Player Player;

    protected ResolvedEffectWithPlayer(Player player)
    {
      Player = player;
    }

    protected override bool Equals(T other)
    {
      return Player.Equals(other.Player);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Player);
    }
  }
}