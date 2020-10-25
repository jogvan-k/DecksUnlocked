using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Types
{
  public static class EqualityComparer
  {
    public static bool Equals<T>(IReadOnlyCollection<T> x, IReadOnlyCollection<T> y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (x.Count != y.Count) return false;
      return x.SequenceEqual(y);
    }

    public static int GetHashCode<T>(IEnumerable<T> sequence)
    {
      var hash = PrimeHashBase;
      if (sequence == null)
        return hash;
      foreach (var entry in sequence)
      {
        hash += PrimeHashBase * hash + entry.GetHashCode();
      }

      return hash;
    }

    static int GetHashCode<T>(IImmutableSet<T> setSequence) where T : IComparable
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
      var hash = PrimeHashBase;
      if (x == null)
        return hash;

      foreach (var kv in x)
      {
        hash += PrimeHashBase * hash + kv.Key.GetHashCode();
        hash += PrimeHashBase * hash + GetHashCode(kv.Value);
      }

      return hash;
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
      var hash = PrimeHashBase;
      if (x == null)
        return hash;

      foreach (var kv in x)
      {
        hash += PrimeHashBase * hash + kv.Key.GetHashCode();
        hash += PrimeHashBase * hash + GetHashCode(kv.Value);
      }

      return hash;
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
      var hash = PrimeHashBase;
      if (x == null)
        return hash;

      foreach (var kv in x)
      {
        hash += PrimeHashBase * hash + kv.Key.GetHashCode();
        hash += PrimeHashBase * hash + GetHashCode(kv.Value);
      }

      return hash;
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
  }
}