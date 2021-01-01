using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
  public interface IMutableStackQueue<T> : IEnumerable<T>
  {
    int Length { get; }
    ImmutableArray<T> Immutable();
    void Enqueue(T item);
    public void Push(T item);
    public T Dequeue();
    bool TryDequeue(out T? value);

    void Clear();
  }
}