using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class CreatureControlMutableStateExtensions
  {
    public static void DamageCreature(
      this IMutableState state,
      IIdentifiable id,
      int damage = 1)
    {
      if (damage < 1) return;

      var creature = state.FindCreature(id, out _, out _);
      creature = creature.Damage(damage);
      state.ResolvedEffects.Add(new CreatureDamaged(creature, damage));
      state.UpdateCreature(creature);
    }

    public static int HealCreature(
      this IMutableState state,
      IIdentifiable id,
      int amount = 1)
    {
      if(amount < 0) return 0;

      var creature = state.FindCreature(id, out _, out _);
      creature = creature.Heal(amount, out var healedAmount);
      if (healedAmount == 0) return 0;
      
      state.ResolvedEffects.Add(new CreatureHealed(creature, healedAmount));
      state.UpdateCreature(creature);
      return healedAmount;
    }

    public static void ReturnCreatureToHand(
      this IMutableState state,
      IIdentifiable id)
    {
      var owningPlayer = state.RemoveCreature(id, out var creature);
      state.Hands[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureReturnedToHand(creature));
      state.RaiseEvent(EventType.CreatureReturnedToHand, creature.Card, owningPlayer);
    }

    public static void AddAemberToCreature(
      this IMutableState state,
      IIdentifiable id,
      int amount = 1)
    {
      if (amount < 1) return;
      var creature = state.FindCreature(id, out _, out _);
      creature.Aember += amount;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new CreatureGainedAember(creature, amount));
    }
    
    public static void UpdateCreature(
      this IMutableState state,
      Creature creature)
    {
      if (creature.Health > 0)
        state.SetCreature(creature);
      else
      {
        DestroyCreature(state, creature);
      }
    }

    public static void StunCreature(
      this IMutableState state,
      IIdentifiable id)
    {
      var creature = state.FindCreature(id, out _, out _);
      if (creature.IsStunned())
        return;
      creature.State = creature.State | CreatureState.Stunned;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new CreatureStunned(creature));
    }

    public static void ExhaustCreature(
      this IMutableState state,
      IIdentifiable id)
    {
      var creature = state.FindCreature(id, out _, out _);
      if (!creature.IsReady)
        return;
      creature.IsReady = false;
      state.SetCreature(creature);
      state.ResolvedEffects.Add(new CreatureExhausted(creature));
    }

    public static void SwapCreatures(
      this IMutableState state,
      IIdentifiable creatureId,
      IIdentifiable targetId)
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
      this IMutableState state,
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

      throw new CreatureNotPresentException(state, creature);
    }

    public static void DestroyCreature(
      this IMutableState state,
      IIdentifiable id)
    {
      var owningPlayer = state.RemoveCreature(id, out var creature);
      state.Discards[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureDied(creature));
      creature.DestroyedAbility?.Invoke(state, creature, owningPlayer);
      state.RaiseEvent(EventType.CreatureDestroyed, creature, owningPlayer);
      
      if (creature.Aember < 1) return;
      state.Aember[owningPlayer.Other()] += creature.Aember;
      state.ResolvedEffects.Add(new AemberClaimed(owningPlayer.Other(), creature.Aember));
    }

    static Player RemoveCreature(
      this IMutableState state,
      IIdentifiable id,
      out Creature creature)
    {
      foreach (var player in state.Fields.Keys)
      {
        foreach (var c in state.Fields[player])
        {
          if (id.Equals(c))
          {
            state.Fields[player].Remove(c);
            creature = c;
            return player;
          }
        }
      }

      throw new CreatureNotPresentException(state, id);
    }
  }
}