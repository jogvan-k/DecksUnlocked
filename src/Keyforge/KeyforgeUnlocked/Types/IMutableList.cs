using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
    public interface IMutableList<T> : IList<T>
    {
        IImmutableList<T> Immutable();
    }
}