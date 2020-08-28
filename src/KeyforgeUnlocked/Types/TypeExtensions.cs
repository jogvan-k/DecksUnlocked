using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  public static class TypeExtensions
  {
    public static IImmutableDictionary<T1, IImmutableStack<T2>> ToImmutable<T1, T2>(
      this IDictionary<T1, Stack<T2>> mutable)
    {
      return mutable.ToImmutableDictionary(kv => kv.Key, kv => (IImmutableStack<T2>) ImmutableStack.Create<T2>(kv.Value.ToArray()));
    }

    public static IImmutableDictionary<T1, IImmutableSet<T2>> ToImmutable<T1, T2>(
      this IDictionary<T1, ISet<T2>> mutable)
    {
      return mutable.ToImmutableDictionary(kv => kv.Key, kv => (IImmutableSet<T2>) kv.Value.ToImmutableHashSet());
    }

    public static IImmutableDictionary<T1, IImmutableList<T2>> ToImmutable<T1, T2>(
      this IDictionary<T1, IList<T2>> mutable)
    {
      return mutable.ToImmutableDictionary(kv => kv.Key, kv => (IImmutableList<T2>) kv.Value.ToImmutableList());
    }

    public static IDictionary<T1, IList<T2>> ToMutable<T1, T2>(
      this IImmutableDictionary<T1, IImmutableList<T2>> immutable)
    {
      return immutable.ToDictionary(kv => kv.Key, kv => (IList<T2>) kv.Value.ToList());
    }

    public static IDictionary<T1, ISet<T2>> ToMutable<T1, T2>(
      this IImmutableDictionary<T1, IImmutableSet<T2>> immutable)
    {
      return immutable.ToDictionary(kv => kv.Key, kv => (ISet<T2>) kv.Value.ToHashSet());
    }

    public static IDictionary<T1, Stack<T2>> ToMutable<T1, T2>(
      this IImmutableDictionary<T1, IImmutableStack<T2>> immutable)
    {
      return immutable.ToDictionary(kv => kv.Key, kv => new Stack<T2>(kv.Value));
    }
  }
}