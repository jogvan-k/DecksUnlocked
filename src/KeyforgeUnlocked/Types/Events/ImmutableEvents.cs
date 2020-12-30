using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.Events
{
  public class ImmutableEvents : EventsBase, IEvents
  {
    public IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks { get; }
    
    public ImmutableEvents()
    {
      EventCallbacks = Initialize();
    }
    
    public ImmutableEvents(IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> immutableDictionary)
    {
      EventCallbacks = immutableDictionary;
    }

    protected override IEnumerable<Callback> GetCallbacks(EventType type)
    {
      return EventCallbacks[type].Values;
    }

    static IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> Initialize()
    {
      return Enum.GetValues<EventType>().ToImmutableDictionary(e => e,
        e => (IImmutableDictionary<string, Callback>) ImmutableDictionary<string, Callback>.Empty);
    }
    
    public Events ToMutable()
    {
      return new Events(EventCallbacks);
    }

    public ImmutableEvents ToImmutable()
    {
      return this;
    }
  }
}