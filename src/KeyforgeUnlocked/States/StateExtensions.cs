using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
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
      IIdentifiable id,
      out Player controllingPlayer,
      out int index)
    {
      if (state.TryFindCreature(id, out controllingPlayer, out index, out var creature))
      {
        return creature;
      }

      throw new CreatureNotPresentException(state, id);
    }

    public static ICard FindCard(
      this IState state,
      IIdentifiable id)
    {
      if(!TryFind(state.Metadata.InitialDecks, id, out _, out _, out var card))
        throw new CardNotPresentException(state, id);

      return card;
    }

    public static bool TryFindCreature(
      this IState state,
      IIdentifiable id,
      out Player controllingPlayer,
      out int index,
      out Creature creature)
    {
      return TryFind(state.Fields, id, out controllingPlayer, out index, out creature);
    }

    static bool TryFind<T>(
      IEnumerable<KeyValuePair<Player, IImmutableList<T>>> toLookup,
      IIdentifiable id,
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
      IIdentifiable id,
      out int index,
      out T lookup) where T : IIdentifiable
    {
      index = 0;
      foreach (var entry in toLookup)
      {
        if (id.Equals(entry))
        {
          lookup = entry;
          return true;
        }

        index++;
      }

      lookup = default;
      return false;
    }

    public static Player ControllingPlayer(this IState state, IIdentifiable creatureId)
    {
      state.FindCreature(creatureId, out var controllingPlayer, out _);
      return controllingPlayer;
    }
  }
}