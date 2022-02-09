using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.Events
{
  public class ImmutableEvents : EventsBase, IEvents
  {
    public IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks { get; }
    public IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> Modifiers { get; }

    public ImmutableEvents()
    {
      EventCallbacks = InitializeEventCallbacks();
      Modifiers = InitializeModifiers();
    }

    public ImmutableEvents(
      IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> immutableDictionary,
      IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> modifiers)
    {
      EventCallbacks = immutableDictionary;
      Modifiers = modifiers;
    }

    protected override IEnumerable<Callback> GetCallbacks(EventType type)
    {
      return EventCallbacks[type].Values;
    }

    protected override IEnumerable<Modifier> GetModifiers(ModifierType type)
    {
      return Modifiers[type].Values;
    }

    static IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> InitializeEventCallbacks()
    {
      return Enum.GetValues<EventType>().ToImmutableDictionary(e => e,
        e => (IImmutableDictionary<string, Callback>) ImmutableDictionary<string, Callback>.Empty);
    }

    IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> InitializeModifiers()
    {
      return Enum.GetValues<ModifierType>().ToImmutableDictionary(m => m,
        m => (IImmutableDictionary<string, Modifier>) ImmutableDictionary<string, Modifier>.Empty);
    }

    public Events ToMutable()
    {
      return new Events(EventCallbacks, Modifiers);
    }

    public ImmutableEvents ToImmutable()
    {
      return this;
    }
  }
}