using System.Collections.Generic;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class MutableStateCardControlExtensions
  {
    public static int Draw(
      this MutableState state,
      Player player,
      int amount = 1)
    {
      if (amount < 1) return 0;
      
      var cardsDrawn = 0;

      while (cardsDrawn < amount && state.Decks[player].Length > 0)
      {
        state.Hands[player].Add(state.Decks[player].Dequeue());
        cardsDrawn++;
      }

      if (cardsDrawn > 0)
        state.ResolvedEffects.Add(new CardsDrawn(player, cardsDrawn));
      
      return cardsDrawn;
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