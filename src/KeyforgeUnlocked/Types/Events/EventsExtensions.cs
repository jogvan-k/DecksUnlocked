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

      void EventDestructor(IMutableState s, IIdentifiable t, Player owningPlayer)
      {
        s.Events.Unsubscribe(source.Id, type);
        s.Events.Unsubscribe(source.Id, EventType.TurnEnded);
      }

      events.Subscribe(source, EventType.TurnEnded, EventDestructor);
    }

    public static void SubscribeUntilLeavesPlay(
      this IMutableEvents events,
      IIdentifiable source,
      EventType type,
      Callback callback)
    {
      events.Subscribe(source, type, callback);

      void EventDestructor(IMutableState s, IIdentifiable t, Player owningPlayer)
      {
        if (!t.Equals(source)) return;
        s.Events.Unsubscribe(source.Id, type);
        s.Events.Unsubscribe(source.Id, EventType.CreatureDestroyed);
        s.Events.Unsubscribe(source.Id, EventType.CreatureReturnedToHand);
      }
      
      events.Subscribe(source, EventType.CreatureDestroyed, EventDestructor);
      events.Subscribe(source, EventType.CreatureReturnedToHand, EventDestructor);
    }
  }
}