using System;
using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithPlayerAndInt<T> : ResolvedEffectWithPlayer<T> where T : ResolvedEffectWithPlayerAndInt<T>
  {
    protected readonly int _int;

    protected ResolvedEffectWithPlayerAndInt(Player player, int @int) : base(player)
    {
      _int = @int;
    }

    protected override bool Equals(T other)
    {
      return base.Equals(other) && _int == other._int;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _int);
    }
  }
}