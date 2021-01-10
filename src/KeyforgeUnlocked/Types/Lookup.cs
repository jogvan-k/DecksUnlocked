using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  /// <summary>
  /// Dictionary-like class that allows manipulation of existing entries, but not to add new entries.
  /// </summary>
  public sealed class Lookup<TKey, TValue> : IMutableLookup<TKey, TValue> where TKey : notnull
  {
    Dictionary<TKey, TValue> _dictionary;
    public TValue this[TKey key]
    {
      get { return _dictionary[key]; }
      set
      {
        if(!_dictionary.Remove(key))
          throw new InvalidOperationException("Adding new keys is not valid.");
        _dictionary.Add(key, value);
      }
    }

    public ImmutableLookup<TKey, TValue> Immutable()
    {
      return new(_dictionary);
    }

    public Lookup(Dictionary<TKey, TValue> dictionary)
    {
      _dictionary = new Dictionary<TKey, TValue>(dictionary);
    }

    public Lookup(IReadOnlyDictionary<TKey, TValue> dictionary)
    {
      _dictionary = dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override int GetHashCode()
    {
      var hc = new HashCode();
      foreach (var keyValue in _dictionary)
      {
        hc.Add(keyValue.Key);
        hc.Add(keyValue.Value);
      }

      return hc.ToHashCode();
    }
    
    bool Equals(Lookup<TKey, TValue> other)
    {
      return Equals(_dictionary, other._dictionary);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Lookup<TKey, TValue>) obj);
    }
  }
}