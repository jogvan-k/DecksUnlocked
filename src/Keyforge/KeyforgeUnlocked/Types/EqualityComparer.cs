using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  public static class EqualityComparer
  {
    public static bool Equals<T>(IList<T> x, IList<T> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (x.Count != y.Count) return false;
      for(int i = 0; i < x.Count; i++)
      {
        if (x[i] == null && y[i] == null) continue;
        if (x[i] == null && y[i] != null) return false;
        if (!x[i].Equals(y[i])) return false;
      }

      return true;
    }

    public static bool Equals<T>(IReadOnlyCollection<T> x, IReadOnlyCollection<T> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (x.Count != y.Count) return false;
      return x.SequenceEqual(y);
    }

    public static int GetHashCode<T>(IEnumerable<T> sequence)
    {
      var hc = new HashCode();
      if (sequence != null)
      {
        foreach (var entry in sequence)
        {
          hc.Add(entry);
        }
      }

      return hc.ToHashCode();
    }

    public static int GetHashCode<T>(IImmutableSet<T> setSequence) where T : IComparable
    {
      return GetHashCode(setSequence.OrderBy(x => x));
    }

    public static bool Equals<T1, T2>(IReadOnlyDictionary<T1, IImmutableStack<T2>> x,
      IReadOnlyDictionary<T1, IImmutableStack<T2>> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (x.Count != y.Count) return false;

      foreach (var kv in x)
      {
        if (!y.TryGetValue(kv.Key, out var yValue) || !kv.Value.SequenceEqual(yValue))
          return false;
      }

      return true;
    }

    public static int GetHashCode<T1, T2>(IReadOnlyDictionary<T1, IImmutableStack<T2>> x)
    {
      var hc = new HashCode();
      if (x != null)
      {
        foreach (var kv in x)
        {
          hc.Add(kv.Key);
          hc.Add(GetHashCode(kv.Value));
        }
      }

      return hc.ToHashCode();
    }

    public static bool Equals<T1, T2, T3>(IReadOnlyDictionary<T1, IImmutableDictionary<T2, T3>> x,
      IReadOnlyDictionary<T1, IImmutableDictionary<T2, T3>> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (x.Count != y.Count) return false;

      foreach (var kv in x)
      {
        if (!y.TryGetValue(kv.Key, out var yValue) || !kv.Value.SequenceEqual(yValue))
          return false;
      }

      return true;
    }

    public static int GetHashCode<T1, T2, T3>(IReadOnlyDictionary<T1, IImmutableDictionary<T2, T3>> x)
    {
      var hc = new HashCode();
      if (x != null)
      {
        foreach (var kv in x.OrderBy(y => y.Key))
        {
          hc.Add(kv.Key);
          hc.Add(GetHashCode(kv.Value));
        }
      }

      return hc.ToHashCode();
    }

    public static bool Equals<T1, T2>(IReadOnlyDictionary<T1, IImmutableSet<T2>> x,
      IReadOnlyDictionary<T1, IImmutableSet<T2>> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      return SetEquals(x, y);
    }

    public static int GetHashCode<T1, T2>(IReadOnlyDictionary<T1, IImmutableSet<T2>> x) where T2 : IComparable
    {
      var hc = new HashCode();
      if (x != null)
      {
        foreach (var kv in x)
        {
          hc.Add(kv.Key);
          hc.Add(GetHashCode(kv.Value));
        }
      }

      return hc.ToHashCode();
    }

    public static bool Equals<T1, T2>(IReadOnlyDictionary<T1, IImmutableList<T2>> x,
      IReadOnlyDictionary<T1, IImmutableList<T2>> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      
      foreach (var kv in x)
      {
        if (!y.TryGetValue(kv.Key, out var yValue) || !kv.Value.SequenceEqual(yValue))
          return false;
      }

      return true;
    }

    public static int GetHashCode<T1, T2>(IReadOnlyDictionary<T1, IImmutableList<T2>> x)
    {

      var hc = new HashCode();
      foreach (var kv in x)
      {
        hc.Add(kv.Key);
        hc.Add(GetHashCode(kv.Value));
      }

      return hc.ToHashCode();
    }

    static bool SetEquals<T1, T2>(IReadOnlyDictionary<T1, IImmutableSet<T2>> first,
      IReadOnlyDictionary<T1, IImmutableSet<T2>> second)
    {
      if (first.Count != second.Count)
        return false;
      foreach (var key in first.Keys)
      {
        if (!second.ContainsKey(key) || !first[key].SetEquals(second[key]))
          return false;
      }

      return true;
    }

    public static int GetHashCode<T1, T2>(IReadOnlyDictionary<T1, T2> dictionary)
    {
      var hashCode = new HashCode();
      foreach (var kv in dictionary.OrderBy(d => d.Key))
      {
        hashCode.Add(kv.Key);
        hashCode.Add(kv.Value);
      }

      return hashCode.ToHashCode();
    }
  }
}