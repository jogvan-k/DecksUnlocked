using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
  public interface IEvents
  {
    IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks { get; }
    void RaiseEvent(EventType type, IMutableState state, IIdentifiable target, Player owningPlayer);

    Events ToMutable();
    ImmutableEvents ToImmutable();
  }
}