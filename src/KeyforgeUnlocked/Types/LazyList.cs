using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Types
{
  public sealed class LazyList<T> : IMutableList<T>
  {
    [NotNull] readonly IImmutableList<T> _initial;
    List<T> _innerList;

    public IImmutableList<T> Immutable()
    {
      if (_innerList != null)
        return _innerList.ToImmutableList();
      return _initial;
    }

    List<T> Mutable()
    {
      if (_innerList == null)
        _innerList = _initial.ToList();
      return _innerList;
    }

    bool Initialized => _innerList != null;

    public LazyList()
    {
      _initial = ImmutableList<T>.Empty;
    }

    public LazyList([NotNull]IEnumerable<T> initial)
    {
      _initial = initial.ToImmutableList();
    }
    public LazyList([NotNull]IImmutableList<T> initial)
    {
      _initial = initial;
    }

    public void Add(T item)
    {
      Mutable().Add(item);
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

    public int IndexOf(T item)
    {
      return Immutable().IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      Mutable().Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      Mutable().RemoveAt(index);
    }

    public T this[int index]
    {
      get => Immutable()[index];
      set => Mutable()[index] = value;
    }
    
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
      return Equals((LazyList<T>) obj);
    }

    bool Equals(LazyList<T> other)
    {
      var first = Initialized ? (IEnumerable<T>) _innerList : _initial;
      var second = other.Initialized ? (IEnumerable<T>) other._innerList : other._initial;

      return first.SequenceEqual(second);
    }

    public override int GetHashCode()
    {
      var hash = PrimeHashBase;
      var entries = Initialized ? (IEnumerable<T>) _innerList : _initial;
      foreach (var entry in entries)
      {
        hash += PrimeHashBase * entry.GetHashCode();
      }
      
      return hash;
    }
  }
}