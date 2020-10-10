using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
  public interface IMutableStack<T>
  {
    IImmutableStack<T> Immutable();
  }
}