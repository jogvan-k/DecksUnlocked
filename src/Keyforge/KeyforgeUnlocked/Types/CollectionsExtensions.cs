using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
    public static class CollectionsExtensions
    {
        public static Lookup<TKey, TValue> ToLookup<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
            where TKey : notnull
        {
            return new(dictionary);
        }

        public static ImmutableLookup<TKey, TElement> ToReadOnly<TKey, TSource, TElement>(
            this IReadOnlyDictionary<TKey, TSource> dictionary,
            Func<KeyValuePair<TKey, TSource>, TElement> valueSelector) where TKey : notnull
        {
            return new(dictionary.ToDictionary(kv => kv.Key, valueSelector));
        }

        public static ImmutableLookup<TKey, TValue> ToReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> lookup)
            where TKey : notnull
        {
            return new(lookup);
        }

        public static ImmutableLookup<TKey, TValue> ToReadOnly<TKey, TValue>(this Lookup<TKey, TValue> lookup)
            where TKey : notnull
        {
            return new(lookup);
        }
    }
}