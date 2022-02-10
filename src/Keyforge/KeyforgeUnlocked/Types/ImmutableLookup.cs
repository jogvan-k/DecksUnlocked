using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
    /// <summary>
    /// Read-only equivalent of <see cref="Lookup{TKey,TValue}"/>.
    /// </summary>
    public sealed class ImmutableLookup<TKey, TValue> : ReadOnlyDictionary<TKey, TValue> where TKey : notnull
    {
        public ImmutableLookup(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public ImmutableLookup(Lookup<TKey, TValue> lookup) : base(lookup.ToDictionary(kv => kv.Key, kv => kv.Value))
        {
        }

        public static ImmutableLookup<TKey, TValue> Empty => new(new Dictionary<TKey, TValue>());
        public override int GetHashCode()
        {
            var hc = new HashCode();
            foreach (var keyValue in Dictionary)
            {
                hc.Add(keyValue.Key);
                hc.Add(keyValue.Value);
            }

            return hc.ToHashCode();
        }

        bool Equals(ImmutableLookup<TKey, TValue> other)
        {
            return EqualityComparer.Equals(Dictionary, other.Dictionary);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ImmutableLookup<TKey, TValue>)obj);
        }

        public Dictionary<TKey, TValue> ToDictionary() => new(Dictionary);
    }
}