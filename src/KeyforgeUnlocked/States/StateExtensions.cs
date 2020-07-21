using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateExtensions
  {
    public static bool Draw(this MutableState state,
      Player player)
    {
      if (state.Decks[player].TryPop(out var card))
      {
        state.Hands[player].Add(card);
        return true;
      }

      return false;
    }

    public static Creature FindCreature(
      this IState state,
      string creatureId,
      out Player controllingPlayer)
    {
      foreach (var player in state.Fields.Keys)
      foreach (var creature in state.Fields[player])
        if (creature.Id.Equals(creatureId))
        {
          controllingPlayer = player;
          return creature;
        }

      throw new CreatureNotPresentException(state, creatureId);
    }
  }
}