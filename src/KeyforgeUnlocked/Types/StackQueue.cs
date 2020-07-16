using System;
using System.Collections;
using System.Collections.Generic;

namespace KeyforgeUnlocked.Types
{
  public class StackQueue<T> : IReadOnlyCollection<T>, IEnumerable<T>
  {
    LinkedList<T> inner;

    public StackQueue()
    {
      inner = new LinkedList<T>();
    }
    public StackQueue(IEnumerable<T> enumerable)
    {
      inner = new LinkedList<T>(enumerable);
    }

    public void Enqueue(T item)
    {
      inner.AddFirst(item);
    }

    public void Push(T item)
    {
      inner.AddLast(item);
    }

    public T Dequeue()
    {
      var value = Peek();
      inner.RemoveLast();
      return value;
    }

    public T Peek()
    {
      var value = inner.Last;
      if (value == null)
        throw new InvalidOperationException("No value present in StackQueue");
      return value.Value;
    }

    public IEnumerator<T> GetEnumerator()
    {
      return inner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int Count => inner.Count;

    protected bool Equals(StackQueue<T> other)
    {
      return Equals(inner, other.inner);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((StackQueue<T>) obj);
    }

    public override int GetHashCode()
    {
      return (inner != null ? inner.GetHashCode() : 0);
    }
  }
}