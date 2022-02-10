using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
    public static class TypeExtensions
    {
        public static ImmutableLookup<T1, IImmutableStack<T2>> ToImmutable<T1, T2>(
            this IReadOnlyDictionary<T1, IMutableStackQueue<T2>> mutable) where T1 : notnull
        {
            return mutable.ToReadOnly(kv => (IImmutableStack<T2>)ImmutableStack.Create(kv.Value.ToArray()));
        }

        public static ImmutableLookup<T1, IImmutableSet<T2>> ToImmutable<T1, T2>(
            this IReadOnlyDictionary<T1, IMutableSet<T2>> mutable) where T1 : notnull
        {
            return mutable.ToReadOnly(kv => kv.Value.Immutable());
        }

        public static ImmutableLookup<T1, IImmutableList<T2>> ToImmutable<T1, T2>(
            this IReadOnlyDictionary<T1, IMutableList<T2>> mutable) where T1 : notnull
        {
            return mutable.ToReadOnly(kv => kv.Value.Immutable());
        }

        public static ImmutableLookup<T1, IMutableList<T2>> ToMutable<T1, T2>(
            this IReadOnlyDictionary<T1, IImmutableList<T2>> immutable) where T1 : notnull
        {
            return immutable.ToReadOnly(kv => (IMutableList<T2>)new LazyList<T2>(kv.Value));
        }

        public static ImmutableLookup<T1, IMutableSet<T2>> ToMutable<T1, T2>(
            this IReadOnlyDictionary<T1, IImmutableSet<T2>> immutable) where T1 : notnull
        {
            return immutable.ToReadOnly(kv => (IMutableSet<T2>)new LazySet<T2>(kv.Value));
        }

        public static ImmutableLookup<T1, IMutableStackQueue<T2>> ToMutable<T1, T2>(
            this IReadOnlyDictionary<T1, IImmutableStack<T2>> immutable) where T1 : notnull
        {
            return immutable.ToReadOnly(kv => (IMutableStackQueue<T2>)new LazyStackQueue<T2>(kv.Value));
        }
    }
}