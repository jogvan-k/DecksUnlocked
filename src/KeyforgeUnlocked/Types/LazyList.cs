using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KeyforgeUnlocked.Types
{
  public class LazyList<T> : IMutableList<T>
  {
    readonly IImmutableList<T> _initial;
    IList<T> _innerList;

    public IList<T> Mutable()
    {
      if (_innerList == null)
        _innerList = _initial.ToList();
      return _innerList;
    }

    public IImmutableList<T> Immutable()
    {
      if (_innerList != null)
        return _innerList.ToImmutableList();
      return _initial;
    }

    public LazyList()
    {
      _initial = ImmutableList<T>.Empty;
    }

    public LazyList(IEnumerable<T> initial)
    {
      _initial = initial.ToImmutableList();
    }
    public LazyList(IImmutableList<T> initial)
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
    public bool IsReadOnly => Mutable().IsReadOnly;

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
  }
}