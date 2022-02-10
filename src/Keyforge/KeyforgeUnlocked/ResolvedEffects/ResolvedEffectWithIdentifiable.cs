using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public abstract class ResolvedEffectWithIdentifiable<T> : Equatable<T>, IResolvedEffect
        where T : ResolvedEffectWithIdentifiable<T>
    {
        public IIdentifiable Id;

        protected ResolvedEffectWithIdentifiable(IIdentifiable id)
        {
            Id = id;
        }

        protected override bool Equals(T other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id);
        }
    }
}