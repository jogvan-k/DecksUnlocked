using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
  public sealed class LazyStackQueue<T> : IMutableStackQueue<T>
  {
    readonly ImmutableArray<T> _initial;
    LinkedList<T>? _innerList;

    public ImmutableArray<T> Immutable()
    {
      if (_innerList != null)
        return _innerList.ToImmutableArray();
      return _initial;
    }

    LinkedList<T> Mutable()
    {
      if (_innerList == null)
        _innerList = new LinkedList<T>(_initial);
      return _innerList;
    }

    public LazyStackQueue()
    {
      _initial = ImmutableArray<T>.Empty;
    }

    public LazyStackQueue(IEnumerable<T> initial)
    {
      _initial = initial.ToImmutableArray();
    }

    public LazyStackQueue(ImmutableArray<T> initial)
    {
      _initial = initial;
    }

    public int Length => Immutable().Length;

    public void Enqueue(T item)
    {
      Mutable().AddFirst(item);
    }

    public void Push(T item)
    {
      Mutable().AddLast(item);
    }

    public T Dequeue()
    {
      var item = Peek();
      Mutable().RemoveLast();
      return item;
    }

    public T Peek()
    {
      var last = Mutable().Last;
      if (last == null)
        throw new InvalidOperationException("Empty StackQueue");
      return last.Value;
    }

    public bool TryDequeue(out T? value)
    {
      if (Mutable().Count == 0)
      {
        value = default;
        return false;
      }

      value = Dequeue();
      return true;
    }

    public void Clear()
    {
      Mutable().Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
      return Mutable().GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
      return ReferenceEquals(this, obj) || obj is LazyStackQueue<T> other && Equals(other);
    }

    bool Equals(LazyStackQueue<T> other)
    {
      return _initial.Equals(other._initial) && _innerList == null && other._innerList == null ||
             Equals(_innerList, other._innerList);
    }

    public override int GetHashCode()
    {
      return Mutable().GetHashCode();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}