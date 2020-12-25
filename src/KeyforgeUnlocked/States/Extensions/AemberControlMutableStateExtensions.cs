﻿using System;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class MutableStateAemberControlExtensions
  {
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
  }
}