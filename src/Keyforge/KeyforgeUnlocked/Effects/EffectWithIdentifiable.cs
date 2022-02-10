using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
    public abstract class EffectWithIdentifiable<T> : EffectBase<T> where T : EffectWithIdentifiable<T>
    {
        public readonly IIdentifiable Id;

        protected EffectWithIdentifiable(IIdentifiable identifiable)
        {
            Id = identifiable;
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