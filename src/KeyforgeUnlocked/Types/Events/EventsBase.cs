using System.Collections.Generic;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.Events
{
  public abstract class EventsBase
  {
    public void RaiseEvent(EventType type, IMutableState state, IIdentifiable target, Player owningPlayer)
    {
      foreach (var e in GetCallbacks(type))
      {
        e.Invoke(state, target, owningPlayer);
      }
    }

    protected abstract IEnumerable<Callback> GetCallbacks(EventType type);

    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj is IEvents e) return Equals(e);
      return false;
    }

    bool Equals(IEvents events)
    {
      var thisEventCallbacks = ((IEvents) this).EventCallbacks;
      return EqualityComparer.Equals(thisEventCallbacks, events.EventCallbacks);
    }

    public override int GetHashCode()
    {
      return EqualityComparer.GetHashCode(((IEvents) this).EventCallbacks);
    }
  }
}