using System;
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

    public int Modify(ModifierType type, IState state)
    {
      var m = 0;
      foreach (var modifier in GetModifiers(type))
      {
        m += modifier.Invoke(state);
      }

      return m;
    }

    protected abstract IEnumerable<Callback> GetCallbacks(EventType type);

    public override bool Equals(object? obj)
    {
      if (obj == null) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj is IEvents e) return Equals(e);
      return false;
    }

    protected abstract IEnumerable<Modifier> GetModifiers(ModifierType type);

    bool Equals(IEvents events)
    {
      var thisEvent = ((IEvents) this);
      return EqualityComparer.Equals(thisEvent.EventCallbacks, events.EventCallbacks)
             && EqualityComparer.Equals(thisEvent.Modifiers, events.Modifiers);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(
        EqualityComparer.GetHashCode(((IEvents) this).EventCallbacks),
        EqualityComparer.GetHashCode((((IEvents) this).Modifiers)));
    }
  }
}