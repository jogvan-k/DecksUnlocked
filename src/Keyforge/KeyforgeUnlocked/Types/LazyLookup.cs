using System.Collections;
using System.Collections.Generic;

namespace KeyforgeUnlocked.Types
{
  public class LazyLookup<TKey, TValue> : IMutableLookup<TKey, TValue> where TKey : notnull
  {
    readonly ImmutableLookup<TKey, TValue> _initial;
    Lookup<TKey, TValue>? _lookup;

    public TValue this[TKey key]
    {
      get => Get()[key];
      set => GetMutable()[key] = value;
    }

    public ImmutableLookup<TKey, TValue> Immutable()
    {
      if (_lookup == null)
        return _initial;
      return new ImmutableLookup<TKey, TValue>(_lookup);
    }

    public LazyLookup(ImmutableLookup<TKey, TValue> initial)
    {
      _initial = initial;
    }

    IReadOnlyDictionary<TKey, TValue> Get()
    {
      if (_lookup != null)
        return _lookup.ToReadOnly();
      return _initial;
    }

    Lookup<TKey, TValue> GetMutable()
    {
      if (_lookup == null)
        _lookup = new Lookup<TKey, TValue>(_initial);
      return _lookup;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return Get().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}