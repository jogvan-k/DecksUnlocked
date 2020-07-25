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
  public static class MutableStateExtensions
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

    public static bool Steal(
      this MutableState state,
      int amount)
    {
      var stealingPlayer = state.PlayerTurn;
      var toSteal = Math.Min(amount, state.Aember[stealingPlayer.Other()]);
      if (toSteal > 0)
      {
        state.Aember[stealingPlayer] += toSteal;
        state.Aember[stealingPlayer.Other()] -= toSteal;
        state.ResolvedEffects.Add(new AemberStolen(stealingPlayer, toSteal));
      }

      return false;
    }

    public static void ReturnFromDiscard(
      this MutableState state,
      string cardId)
    {
      if (!TryRemove(state.Discards, cardId, out var owningPlayer, out var card))
        throw new CardNotPresentException(state, cardId);

      state.Hands[owningPlayer].Add(card);
      state.ResolvedEffects.Add(new CardReturnedToHand(card));
    }

    public static void UpdateCreature(
      this MutableState state,
      Creature creature)
    {
      if (creature.Health > 0)
        state.SetCreature(creature);
      else
      {
        DestroyCreature(state, creature);
        creature.DestroyedAbility?.Invoke(state, creature.Id);
      }
    }

    public static void SwapCreatures(
      this MutableState state,
      string creatureId,
      string targetId)
    {
      foreach (var player in state.Fields.Keys)
      {
        var field = state.Fields[player];
        var ci = field.Index(creatureId);
        if (ci == -1)
          continue;
        var ti = field.Index(targetId);
        if (ti == -1)
          throw new CreatureNotPresentException(state, targetId);
        var creature = field[ci];
        var target = field[ti];
        field[ti] = creature;
        field[ci] = target;
        return;
      }

      throw new CreatureNotPresentException(state, creatureId);
    }

    public static void SetCreature(
      this MutableState state,
      Creature creature)
    {
      foreach (var keyValue in state.Fields)
      {
        var creatures = keyValue.Value;
        for (int i = 0; i < creatures.Count; i++)
        {
          if (creatures[i].Id == creature.Id)
          {
            creatures[i] = creature;
            return;
          }
        }
      }

      throw new CreatureNotPresentException(state, creature.Id);
    }

    public static void DestroyCreature(
      this MutableState state,
      Creature creature)
    {
      var owningPlayer = RemoveCreature(state, creature);
      state.Discards[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureDied(creature));
    }

    static Player RemoveCreature(
      MutableState state,
      Creature creature)
    {
      foreach (var player in state.Fields.Keys)
      {
        foreach (var c in state.Fields[player])
        {
          if (c.Id == creature.Id)
          {
            state.Fields[player].Remove(c);
            return player;
          }
        }
      }

      throw new CreatureNotPresentException(state, creature.Id);
    }

    static bool TryRemove<T>(IDictionary<Player, ISet<T>> toLookup,
      string id,
      out Player owningPlayer,
      out T lookup) where T : IIdentifiable
    {
      foreach (var keyValue in toLookup)
      {
        if (TryRemove(keyValue.Value, id, out lookup))
        {
          owningPlayer = keyValue.Key;
          return true;
        }
      }

      owningPlayer = default;
      lookup = default;
      return false;
    }

    static bool TryRemove<T>(ICollection<T> toLookup,
      string id,
      out T lookup) where T : IIdentifiable
    {
      foreach (var item in toLookup)
      {
        if (item.Id.Equals(id))
        {
          lookup = item;
          toLookup.Remove(item);
          return true;
        }
      }

      lookup = default;
      return false;
    }
  }
}