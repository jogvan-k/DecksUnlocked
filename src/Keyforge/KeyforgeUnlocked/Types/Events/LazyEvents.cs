using System.Collections.Immutable;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
    public class LazyEvents : IMutableEvents
    {
        readonly ImmutableEvents _initial;
        Events? _events;

        public IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks =>
            Get().EventCallbacks;

        public IImmutableDictionary<ModifierType, IImmutableDictionary<string, Modifier>> Modifiers =>
            Get().Modifiers;

        public LazyEvents()
        {
            _initial = new ImmutableEvents();
        }

        public LazyEvents(ImmutableEvents initial)
        {
            _initial = initial;
        }

        public void RaiseEvent(EventType type, IMutableState state, IIdentifiable target, Player owningPlayer)
        {
            Get().RaiseEvent(type, state, target, owningPlayer);
        }

        public int Modify(ModifierType type, IState state)
        {
            return Get().Modify(type, state);
        }

        public void Subscribe(IIdentifiable source, EventType type, Callback callback)
        {
            GetMutable().Subscribe(source, type, callback);
        }

        public void Subscribe(IIdentifiable source, ModifierType type, Modifier modifier)
        {
            GetMutable().Subscribe(source, type, modifier);
        }

        public void Unsubscribe(string id)
        {
            GetMutable().Unsubscribe(id);
        }

        public Events ToMutable()
        {
            return Get().ToMutable();
        }

        public ImmutableEvents ToImmutable()
        {
            return Get().ToImmutable();
        }

        IEvents Get()
        {
            if (_events == null) return _initial;
            return _events;
        }

        IMutableEvents GetMutable()
        {
            return _events ??= _initial.ToMutable();
        }
    }
}