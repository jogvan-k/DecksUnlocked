using System;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
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

    public static void UpdateCreature(
      this MutableState state,
      Creature creature)
    {
      if (creature.Health > 0)
        CreatureUtil.SetCreature(state, creature);
      else
      {
        CreatureUtil.DestroyCreature(state, creature);
      }
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