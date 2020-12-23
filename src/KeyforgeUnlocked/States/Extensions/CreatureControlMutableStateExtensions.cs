using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class CreatureControlMutableStateExtensions
  {
    public static void DamageCreature(
      this MutableState state,
      Creature creature,
      int damage = 1)
    {
      if (damage < 1) return;
      creature.Damage += damage;
      state.ResolvedEffects.Add(new CreatureDamaged(creature, damage));
      state.UpdateCreature(creature);
    }
    public static void ReturnCreatureToHand(
      this MutableState state,
      Creature creature)
    {
      var owningPlayer = state.RemoveCreature(creature);
      state.Hands[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureReturnedToHand(creature));
    }

    public static void AddAemberToCreature(
      this MutableState state,
      string creatureId,
      int amount = 1)
    {
      if (amount < 1) return;
      var creature = state.FindCreature(creatureId, out _, out _);
      creature.Aember += amount;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new CreatureGainedAember(creature, amount));
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
  }
}