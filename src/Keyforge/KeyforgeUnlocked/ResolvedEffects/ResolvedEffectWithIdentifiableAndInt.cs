using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithIdentifiableAndInt<T> : ResolvedEffectWithIdentifiable<T> where T : ResolvedEffectWithIdentifiableAndInt<T>
  {
    public readonly int Int;

    protected ResolvedEffectWithIdentifiableAndInt(IIdentifiable id, int @int) : base(id)
    {
      Int = @int;
    }

    protected override bool Equals(T other)
    {
      return base.Equals(other) && Int == other.Int;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Int) ;
    }
  }
}