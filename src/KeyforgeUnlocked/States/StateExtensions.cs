using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public static class StateExtensions
  {
    public static Creature FindCreature(
      this IState state,
      string creatureId,
      out Player controllingPlayer, out int index)
    {
      if (TryFind(state.Fields, creatureId, out controllingPlayer, out index, out var creature))
      {
        return creature;
      }

      throw new CreatureNotPresentException(state, creatureId);
    }

    static bool TryFind<T>(
      IEnumerable<KeyValuePair<Player, IImmutableList<T>>> toLookup,
      string id,
      out Player owningPlayer,
      out int index,
      out T lookup) where T : IIdentifiable
    {
      foreach (var keyValue in toLookup)
      {
        if (TryFind(keyValue.Value, id, out index, out lookup))
        {
          owningPlayer = keyValue.Key;
          return true;
        }
      }

      owningPlayer = default;
      index = default;
      lookup = default;
      return false;
    }

    static bool TryFind<T>(IEnumerable<T> toLookup,
      string id,
      out int index,
      out T lookup) where T : IIdentifiable
    {
      index = 0;
      foreach (var entry in toLookup)
      {
        if (entry.Id.Equals(id))
        {
          lookup = entry;
          return true;
        }

        index++;
      }

      lookup = default;
      return false;
    }

    public static Player ControllingPlayer(this IState state, Creature creature)
    {
      state.FindCreature(creature.Id, out var controllingPlayer, out _);
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
        if (currentState.PreviousState == null)
          return false;
        currentState = (IState) currentState.PreviousState.Value;
      }

      return false;
    }
  }
}