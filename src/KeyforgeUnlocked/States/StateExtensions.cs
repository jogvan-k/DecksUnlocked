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
    public static MutableState ToMutable(this IState state)
    {
      return new MutableState(
        state.PlayerTurn,
        state.TurnNumber,
        new Dictionary<Player, Stack<Card>>(state.Decks),
        new Dictionary<Player, ISet<Card>>(state.Hands),
        new Dictionary<Player, ISet<Card>>(state.Discards),
        new Dictionary<Player, ISet<Card>>(state.Archives),
        new Dictionary<Player, IList<Creature>>(state.Fields),
        new Queue<Effect>());
    }

    public static void Draw(this MutableState state, Player player)
    {
      if (state.Decks[player].TryPop(out var card))
        state.Hands[player].Add(card);
    }
  }
}