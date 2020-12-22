using System;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithPlayer<T> : Equatable<T>, IResolvedEffect where T : ResolvedEffectWithPlayer<T>
  {
    protected readonly Player _player;

    protected ResolvedEffectWithPlayer(Player player)
    {
      _player = player;
    }

    protected override bool Equals(T other)
    {
      return _player.Equals(other._player);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _player);
    }
  }
}