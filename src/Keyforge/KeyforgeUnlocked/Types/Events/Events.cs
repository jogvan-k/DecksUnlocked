using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KeyforgeUnlocked.Types.Events
{
    public class Events : EventsBase, IMutableEvents
    {
        readonly IImmutableDictionary<EventType, IDictionary<string, Callback>> EventCallbacks;
        readonly IImmutableDictionary<ModifierType, IDictionary<string, Modifier>> Modifiers;

        public Events()
        {
            EventCallbacks = InitializeEventCallbacks();
            Modifiers = InitializeModifiers();
        }

        public Events(IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> eventCallbacks,
            IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> modifiers)
        {
            EventCallbacks = eventCallbacks.ToImmutableDictionary(e => e.Key,
                e => (IDictionary<string, Callback>)new Dictionary<string, Callback>(e.Value));
            Modifiers = modifiers.ToImmutableDictionary(m => m.Key,
                m => (IDictionary<string, Modifier>)new Dictionary<string, Modifier>(m.Value));
        }

        public void Subscribe(IIdentifiable source, EventType type, Callback callback)
        {
            Get(type).Add(source.Id, callback);
        }

        public void Subscribe(IIdentifiable source, ModifierType type, Modifier modifier)
        {
            Get(type).Add(source.Id, modifier);
        }

        public void Unsubscribe(string id)
        {
            foreach (var e in EventCallbacks.Keys)
            {
                EventCallbacks[e].Remove(id);
            }

            foreach (var m in Modifiers.Keys)
            {
                Modifiers[m].Remove(id);
            }
        }

        IDictionary<string, Callback> Get(EventType type)
        {
            return EventCallbacks[type];
        }

        IDictionary<string, Modifier> Get(ModifierType type)
        {
            return Modifiers[type];
        }

        protected override IEnumerable<Callback> GetCallbacks(EventType type)
        {
            return EventCallbacks[type].Values;
        }

        protected override IEnumerable<Modifier> GetModifiers(ModifierType type)
        {
            return Modifiers[type].Values;
        }

        IImmutableDictionary<EventType, IDictionary<string, Callback>> InitializeEventCallbacks()
        {
            return Enum.GetValues<EventType>().ToImmutableDictionary(e => e,
                e => (IDictionary<string, Callback>)new Dictionary<string, Callback>());
        }

        IImmutableDictionary<ModifierType, IDictionary<string, Modifier>> InitializeModifiers()
        {
            return Enum.GetValues<ModifierType>().ToImmutableDictionary(m => m,
                m => (IDictionary<string, Modifier>)new Dictionary<string, Modifier>());
        }

        IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> IEvents.EventCallbacks =>
            EventCallbacks.ToImmutableDictionary(e => e.Key,
                e => (IImmutableDictionary<string, Callback>)e.Value.ToImmutableDictionary());

        IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> IEvents.Modifiers =>
            Modifiers.ToImmutableDictionary(m => m.Key,
                m => (IImmutableDictionary<string, Modifier>)m.Value.ToImmutableDictionary());

        public Events ToMutable()
        {
            return new(((IEvents)this).EventCallbacks, (((IEvents)this).Modifiers));
        }

        public ImmutableEvents ToImmutable()
        {
            return new(((IEvents)this).EventCallbacks, ((IEvents)this).Modifiers);
        }
    }
}