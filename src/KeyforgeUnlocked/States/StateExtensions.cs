using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateExtensions
  {
    public static void Draw(this MutableState state, Player player)
    {
      if (state.Decks[player].TryPop(out var card))
        state.Hands[player].Add(card);
    }
  }
}