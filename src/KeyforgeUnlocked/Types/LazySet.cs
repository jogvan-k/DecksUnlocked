using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  public sealed class LazySet<T> : IMutableSet<T>
  {
    readonly IImmutableSet<T> _initial;
    HashSet<T> _innerSet;

    public IImmutableSet<T> Immutable()
    {
      if (_innerSet != null)
        return _innerSet.ToImmutableHashSet();
      return _initial;
    }

    HashSet<T> Mutable()
    {
      if (_innerSet == null)
        _innerSet = _initial.ToHashSet();
      return _innerSet;
    }

    public LazySet()
    {
      _initial = ImmutableHashSet<T>.Empty;
    }

    public LazySet(IEnumerable<T> initial)
    {
      _initial = initial.ToImmutableHashSet();
    }

    public LazySet(IImmutableSet<T> initial)
    {
      _initial = initial;
    }
    public void ExceptWith(IEnumerable<T> other)
    {
      Mutable().ExceptWith(other);
    }

    public void IntersectWith(IEnumerable<T> other)
    {
      Mutable().IntersectWith(other);
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      return Immutable().IsProperSubsetOf(other);
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      return Mutable().IsProperSupersetOf(other);
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
      return Immutable().IsSubsetOf(other);
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
      return Immutable().IsSupersetOf(other);
    }

    public bool Overlaps(IEnumerable<T> other)
    {
      return Immutable().Overlaps(other);
    }

    public bool SetEquals(IEnumerable<T> other)
    {
      return Immutable().SetEquals(other);
    }

    public void SymmetricExceptWith(IEnumerable<T> other)
    {
      Mutable().SymmetricExceptWith(other);
    }

    public void UnionWith(IEnumerable<T> other)
    {
      Mutable().UnionWith(other);
    }

    bool ISet<T>.Add(T item)
    {
      return Mutable().Add(item);
    }

    public void Add(T item)
    {
      ((ICollection<T>) Mutable()).Add(item);
    }

    public void Clear()
    {
      Mutable().Clear();
    }

    public bool Contains(T item)
    {
      return Immutable().Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      Mutable().CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
      return Mutable().Remove(item);
    }

    public int Count => Immutable().Count;

    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator()
    {
      return Immutable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((LazySet<T>) obj);
    }

    bool Equals(LazySet<T> other)
    {
      return Equals(_initial, other._initial) && _innerSet == null && other._innerSet == null || Equals(_innerSet, other._innerSet);
    }

    public override int GetHashCode()
    {
      return Mutable().GetHashCode();
    }
  }
}