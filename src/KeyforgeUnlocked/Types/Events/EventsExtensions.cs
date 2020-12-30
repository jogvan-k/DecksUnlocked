using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
  public static class EventsExtensions
  {
    public static void SubscribeUntil(
      this IMutableEvents events,
      IIdentifiable source,
      EventType type,
      Callback callback,
      EventType untilType)
    {
      events.Subscribe(source, type, callback);

      void EventDestructor(IMutableState s, IIdentifiable t, Player owningPlayer)
      {
        s.Events.Unsubscribe(source.Id, type);
        s.Events.Unsubscribe(source.Id, untilType);
      }

      events.Subscribe(source, untilType, EventDestructor);
    }
  }
}