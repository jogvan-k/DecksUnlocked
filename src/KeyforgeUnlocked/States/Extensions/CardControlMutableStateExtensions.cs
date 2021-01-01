using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;
using static KeyforgeUnlocked.States.Extensions.ExtensionsUtil;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class CardControlMutableStateExtensions
  {
    public static int Draw(
      this IMutableState state,
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

    public static void ArchiveFromHand(
      this IMutableState state,
      IIdentifiable id)
    {
      if (!TryRemove(state.Hands, id, out var player, out var card) || card == null)
        throw new CardNotPresentException(state, id);

      state.Archives[player].Add(card);
      state.ResolvedEffects.Add(new CardArchived(card));
    }

    public static void PopArchive(this IMutableState state)
    {
      var archive = state.Archives[state.PlayerTurn];
      if (archive.Count == 0) return;
      
      foreach (var archivedCard in archive)
      {
        state.Hands[state.PlayerTurn].Add(archivedCard);
      }
      
      archive.Clear();
      state.ResolvedEffects.Add(new ArchivedClaimed());
    }

    public static void ReturnFromDiscard(
      this IMutableState state,
      IIdentifiable id)
    {
      if (!TryRemove(state.Discards, id, out var owningPlayer, out var card) || card == null)
        throw new CardNotPresentException(state, id);

      state.Hands[owningPlayer].Add(card);
      state.ResolvedEffects.Add(new CardReturnedToHand(card));
    }

    public static void PurgeFromDiscard(
      this IMutableState state,
      IIdentifiable id)
    {
      if (!TryRemove(state.Discards, id, out var player, out var card) || card == null)
        throw new CardNotPresentException(state, id);

      state.PurgeCard(player, card);
    }

    public static void PurgeCard(
      this IMutableState state,
      Player player,
      ICard card)
    {
      state.PurgedCard[player].Add(card);
      state.ResolvedEffects.Add(new CardPurged(card));
    }

    public static ICard? DiscardTopOfDeck(
      this IMutableState state)
    {
      var player = state.PlayerTurn;
      if (!state.Decks[player].TryDequeue(out var discardedCard) || discardedCard == null)
        return null;

      state.Discards[player].Add(discardedCard);
      state.ResolvedEffects.Add(new CardDiscarded(discardedCard));
      return discardedCard;
    }
  }
}