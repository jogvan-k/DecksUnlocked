using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types
{
    public interface IMutableSet<T> : ISet<T>
    {
        IImmutableSet<T> Immutable();
    }
}