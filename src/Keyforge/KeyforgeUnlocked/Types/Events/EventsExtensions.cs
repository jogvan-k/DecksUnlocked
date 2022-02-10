using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
    public static class EventsExtensions
    {
        public static void SubscribeUntilEndOfTurn(
            this IMutableEvents events,
            IIdentifiable source,
            EventType type,
            Callback callback)
        {
            events.Subscribe(source, type, callback);
            events.Subscribe(source,
                EventType.TurnEnded,
                (s, _, _) => { s.Events.Unsubscribe(source.Id); });
        }

        public static void SubscribeUntilLeavesPlay(
            this IMutableEvents events,
            IIdentifiable source,
            EventType type,
            Callback callback)
        {
            events.Subscribe(source, type, callback);

            events.Subscribe(source, EventType.CreatureDestroyed, EventDestructor(source));
            events.Subscribe(source, EventType.CreatureReturnedToHand, EventDestructor(source));
        }

        public static void SubscribeUntilLeavesPlay(
            this IMutableEvents events,
            IIdentifiable source,
            ModifierType type,
            Modifier modifier)
        {
            events.Subscribe(source, type, modifier);

            events.Subscribe(source, EventType.CreatureDestroyed, EventDestructor(source));
            events.Subscribe(source, EventType.CreatureReturnedToHand, EventDestructor(source));
        }

        static Callback EventDestructor(IIdentifiable source)
        {
            return (s, t, _) =>
            {
                if (t != null && t.Equals(source)) s.Events.Unsubscribe(source.Id);
            };
        }
    }
}