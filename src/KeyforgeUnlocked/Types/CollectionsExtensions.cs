using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  public static class CollectionsExtensions
  {
    public static Lookup<TKey, TValue> ToLookup<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
    {
      return new Lookup<TKey, TValue>(dictionary);
    }

    public static LookupReadOnly<TKey, TElement> ToReadOnly<TKey, TSource, TElement>(
      this IReadOnlyDictionary<TKey, TSource> dictionary, Func<KeyValuePair<TKey, TSource>, TElement> valueSelector)
    {
      return new LookupReadOnly<TKey, TElement>(dictionary.ToDictionary(kv => kv.Key, valueSelector));
    }

    public static LookupReadOnly<TKey, TValue> ToReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> lookup)
    {
      return new LookupReadOnly<TKey, TValue>(lookup);
    }
    
    public static LookupReadOnly<TKey, TValue> ToReadOnly<TKey, TValue>(this Lookup<TKey, TValue> lookup)
    {
      return new LookupReadOnly<TKey, TValue>(lookup);
    }
  }
}