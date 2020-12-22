using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public static class MutableStateExtensions
  {
    public static bool Draw(this MutableState state,
      Player player)
    {
      if (state.Decks[player].TryDequeue(out var card))
      {
        state.Hands[player].Add(card);
        return true;
      }

      return false;
    }

    #region Aember control

    public static void StealAember(
      this MutableState state,
      Player stealingPlayer,
      int amount = 1)
    {
      var toSteal = Math.Min(amount, state.Aember[stealingPlayer.Other()]);
      if (toSteal < 1) return; 
      state.Aember[stealingPlayer] += toSteal;
      state.Aember[stealingPlayer.Other()] -= toSteal;
      state.ResolvedEffects.Add(new AemberStolen(stealingPlayer, toSteal));
    }
    public static void GainAember(
      this MutableState state,
      Player player,
      int amount = 1)
    {
      if (amount < 1) return;
      state.Aember[player] += amount;
      state.ResolvedEffects.Add(new AemberGained(player, amount));
    }

    public static void LoseAember(
      this MutableState state,
      Player player,
      int amount = 1)
    {
      var toLose = Math.Min(state.Aember[player], amount);
      if (toLose < 1) return;
      state.Aember[player] -= toLose;
      state.ResolvedEffects.Add(new AemberLost(player, toLose));
    }

    public static void CaptureAember(
      this MutableState state,
      string creatureId,
      int amount = 1)
    {
      var creature = state.FindCreature(creatureId, out var controllingPlayer, out _);
      var toCapture = Math.Min(state.Aember[controllingPlayer.Other()], amount);
      if (toCapture < 1) return;
      state.Aember[controllingPlayer.Other()] -= toCapture;
      creature.Aember += toCapture;
      state.ResolvedEffects.Add(new AemberCaptured(creature, toCapture));
      state.UpdateCreature(creature);
    }

    #endregion

    public static void ReturnFromDiscard(
      this MutableState state,
      string cardId)
    {
      if (!TryRemove(state.Discards, cardId, out var owningPlayer, out var card))
        throw new CardNotPresentException(state, cardId);

      state.Hands[owningPlayer].Add(card);
      state.ResolvedEffects.Add(new CardReturnedToHand(card));
    }

    public static void ReturnCreatureToHand(
      this MutableState state,
      Creature creature)
    {
      var owningPlayer = state.RemoveCreature(creature);
      state.Hands[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureReturnedToHand(creature));
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

        state.ResolvedEffects.Add(new CreaturesSwapped(creature, target));
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
      var owningPlayer = state.RemoveCreature(creature);
      state.Discards[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureDied(creature));
      if (creature.Aember < 1) return;
      state.Aember[owningPlayer.Other()] += creature.Aember;
      state.ResolvedEffects.Add(new AemberClaimed(owningPlayer.Other(), creature.Aember));
    }

    static Player RemoveCreature(
      this MutableState state,
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

    static bool TryRemove<T>(IReadOnlyDictionary<Player, IMutableSet<T>> toLookup,
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