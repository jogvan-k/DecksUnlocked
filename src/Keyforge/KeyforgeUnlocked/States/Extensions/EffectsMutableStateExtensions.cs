using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class EffectsMutableStateExtensions
  {
    public static void AddEffect(
      this IMutableState state,
      IEffect effect)
    {
      state.Effects.Push(effect);
    }

    public static void RaiseEvent(
      this IMutableState state,
      EventType @event,
      IIdentifiable? target = null,
      Player owningPlayer = Player.None)
    {
      state.Events.RaiseEvent(@event, state, target!, owningPlayer);
    }

    public static int Modify(
      this IMutableState state,
      ModifierType type)
    {
      return state.Events.Modify(type, state);
    }
  }
}