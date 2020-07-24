using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateExtensions
  {
    public static Creature FindCreature(
      this IState state,
      string creatureId,
      out Player controllingPlayer)
    {
      if (TryFind(state.Fields, creatureId, out controllingPlayer, out var creature))
      {
        return creature;
      }

      throw new CreatureNotPresentException(state, creatureId);
    }

    static bool TryFind<T>(
      IEnumerable<KeyValuePair<Player, IList<T>>> toLookup,
      string id,
      out Player owningPlayer,
      out T lookup) where T : IIdentifiable
    {
      foreach (var keyValue in toLookup)
      {
        if (TryFind(keyValue.Value, id, out lookup))
        {
          owningPlayer = keyValue.Key;
          return true;
        }
      }

      owningPlayer = default;
      lookup = default;
      return false;
    }

    static bool TryFind<T>(IEnumerable<T> toLookup,
      string id,
      out T lookup) where T : IIdentifiable
    {
      foreach (var entry in toLookup)
      {
        if (entry.Id.Equals(id))
        {
          lookup = entry;
          return true;
        }
      }

      lookup = default;
      return false;
    }

    public static Player ControllingPlayer(this IState state, Creature creature)
    {
      state.FindCreature(creature.Id, out var controllingPlayer);
      return controllingPlayer;
    }

    public static bool HasEffectOccured(this IState state, Predicate<IResolvedEffect> predicate, int fromTurn = 0,
      int toTurn = 0)
    {
      if (toTurn < fromTurn)
        throw new ArgumentException("Argument fromTurn must be smaller or equal to toTurn");
      var currentState = state;
      var maxTurnNumber = currentState.TurnNumber - fromTurn;
      var minTurnNumber = currentState.TurnNumber - toTurn;
      while (currentState != null && currentState.TurnNumber >= minTurnNumber)
      {
        if (currentState.TurnNumber <= maxTurnNumber && currentState.ResolvedEffects.Any(re => predicate(re)))
          return true;
        currentState = currentState.PreviousState;
      }

      return false;
    }
  }
}