using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.Events
{
  public class Events : EventsBase, IMutableEvents
  {
    readonly IImmutableDictionary<EventType, IDictionary<string, Callback>> EventCallbacks;

    public Events()
    {
      EventCallbacks = Initialize();
    }

    public Events(IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> eventCallbacks)
    {
      EventCallbacks = eventCallbacks.ToImmutableDictionary(e => e.Key,
        e => (IDictionary<string, Callback>) new Dictionary<string, Callback>(e.Value));
    }

    public void Subscribe(IIdentifiable source, EventType type, Callback callback)
    {
      Get(type).Add(source.Id, callback);
    }

    public void Unsubscribe(string id, EventType type)
    {
      Get(type).Remove(id);
    }

    IDictionary<string, Callback> Get(EventType type)
    {
      return EventCallbacks[type];
    }

    protected override IEnumerable<Callback> GetCallbacks(EventType type)
    {
      return EventCallbacks[type].Values;
    }

    IImmutableDictionary<EventType, IDictionary<string, Callback>> Initialize()
    {
      return Enum.GetValues<EventType>().ToImmutableDictionary(e => e,
        e => (IDictionary<string, Callback>) new Dictionary<string, Callback>());
    }

    IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> IEvents.EventCallbacks =>
      EventCallbacks.ToImmutableDictionary(e => e.Key,
        e => (IImmutableDictionary<string, Callback>) e.Value.ToImmutableDictionary());

    public Events ToMutable()
    {
      return new (((IEvents) this).EventCallbacks);
    }

    public ImmutableEvents ToImmutable()
    {
      return new(((IEvents) this).EventCallbacks);
    }
  }
}