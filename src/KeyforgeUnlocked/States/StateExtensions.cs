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
    public static MutableState ToMutable(this State state)
    {
      return new MutableState(
        state.PlayerTurn,
        state.TurnNumber,
        new Dictionary<Player, Card[]>(state.Decks),
        new Dictionary<Player, Card[]>(state.Hands),
        new Dictionary<Player, Card[]>(state.Discards),
        new Dictionary<Player, Card[]>(state.Archives),
        new Dictionary<Player, List<Creature>>(state.Fields),
        new Queue<Effect>());
    }

    public static bool TryReduce(this Dictionary<Player, Card[]> cards, Player player, int drawAmount,
      out Dictionary<Player, Card[]> remaining, out Card[] reducedCards)
    {
      var currentRemaining = cards[player].Length;
      var toDraw = Math.Min(drawAmount, currentRemaining);
      if (toDraw > 0)
      {
        var remainingCards = new Card[currentRemaining - toDraw];
        Array.Copy(cards[player], remainingCards, toDraw);
        remaining = new Dictionary<Player, Card[]>
        {
          {player, remainingCards}, {player.Other(), cards[player]}
        };
        reducedCards = new Card[toDraw];
        Array.Copy(cards[player], currentRemaining - toDraw, reducedCards, 0, drawAmount);
        return toDraw == drawAmount;
      }

      remaining = cards;
      reducedCards = new Card[0];
      return false;
    }

    public static T[] Concat<T>(this T[] first, T[] second)
    {
      var newArray = new T[first.Length + second.Length];
      first.CopyTo(newArray, 0);
      second.CopyTo(newArray, first.Length);
      return newArray;
    }
  }
}