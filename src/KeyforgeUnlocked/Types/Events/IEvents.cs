using System.Collections.Immutable;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
  public interface IEvents
  {
    IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks { get; }
    IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> Modifiers { get; }
    void RaiseEvent(EventType type, IMutableState state, IIdentifiable target, Player owningPlayer);
    int Modify(ModifierType type, IState state);
    Events ToMutable();
    ImmutableEvents ToImmutable();
  }
}