using System.Collections.Immutable;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
  public class LazyEvents : IMutableEvents
  {
    readonly ImmutableEvents _initial;
    Events _events;

    public IImmutableDictionary<EventType, IImmutableDictionary<string, Callback>> EventCallbacks =>
      Get().EventCallbacks;

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

    public void Subscribe(IIdentifiable source, EventType type, Callback callback)
    {
      GetMutable().Subscribe(source, type, callback);
    }

    public void Unsubscribe(string id, EventType type)
    {
      GetMutable().Unsubscribe(id, type);
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
      return (IEvents) _events ?? _initial;
    }

    IMutableEvents GetMutable()
    {
      return _events ??= _initial.ToMutable();
    }
  }
}