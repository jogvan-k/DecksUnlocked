using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Types
{
  public sealed class LookupReadOnly<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>
  {
    public LookupReadOnly(IDictionary<TKey, TValue> dictionary) : base(dictionary)
    {
    }

    public LookupReadOnly(Lookup<TKey, TValue> lookup) : base(lookup.ToDictionary(kv => kv.Key, kv => kv.Value))
    {
    }
    
    public override int GetHashCode()
    {
      var hash = 0;
      foreach (var keyValue in Dictionary)
      {
        hash += PrimeHashBase * hash + keyValue.Key.GetHashCode();
        hash += PrimeHashBase * hash + keyValue.Value.GetHashCode();
      }

      return hash;
    }
    bool Equals(LookupReadOnly<TKey, TValue> other)
    {
      return Equals(Dictionary, other.Dictionary);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((LookupReadOnly<TKey, TValue>) obj);
    }
  }
}