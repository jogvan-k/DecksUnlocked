using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
  public interface IMutableStackQueue<T> : IEnumerable<T>
  {
    int Length { get; }
    ImmutableArray<T> Immutable();
    void Enqueue(T item);
    void Enqueue(IEnumerable<T> items);
    public void Push(T item);
    void Push(IEnumerable<T> items);
    public T Dequeue();
    bool TryDequeue(out T? value);
    void ReplaceWith(IEnumerable<T> t);
    void Clear();
  }
}