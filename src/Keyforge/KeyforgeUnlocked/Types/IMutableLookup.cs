using System.Collections.Generic;

namespace KeyforgeUnlocked.Types
{
  public interface IMutableLookup<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
  {
    TValue this[TKey key] { get; set; }

    ImmutableLookup<TKey, TValue> Immutable();
  }
}