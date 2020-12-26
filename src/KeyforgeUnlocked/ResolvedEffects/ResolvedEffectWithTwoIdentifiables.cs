using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithTwoIdentifiables<T> : ResolvedEffectWithIdentifiable<T> where T : ResolvedEffectWithTwoIdentifiables<T>
  {
    public IIdentifiable Target;

    public ResolvedEffectWithTwoIdentifiables(IIdentifiable id, IIdentifiable target) : base(id)
    {
      Target = target;
    }

    protected override bool Equals(T other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Target);
    }
  }
}