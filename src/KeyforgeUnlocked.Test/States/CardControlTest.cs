using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class CardControlTest
  {
    [Test]
    public void Draw(
      [Values(Player.Player1, Player.Player2)]
      Player player,
      [Range(0, 3)] int amount)
    {
      var state = StateTestUtil.EmptyState.New(decks: SetupDecks());

      var cardsDrawn = state.Draw(player, amount);

      var expectedDecks = SetupDecks();
      var expectedHands = TestUtil.Sets<ICard>();
      var expectedResolvedEffects = new LazyList<IResolvedEffect>();
      for (int i = 0; i < amount && i < 2; i++)
      {
        expectedHands[player].Add(expectedDecks[player].Dequeue());
      }

      var expectedCardsDrawn = Math.Min(2, amount);
      if (expectedCardsDrawn > 0)
        expectedResolvedEffects.Add(new CardsDrawn(player, expectedCardsDrawn));
      var expectedState = StateTestUtil.EmptyState.New(decks: expectedDecks, hands: expectedHands,
        resolvedEffects: expectedResolvedEffects);
      Assert.That(cardsDrawn, Is.EqualTo(expectedCardsDrawn));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ReturnFromDiscards(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 1)] int cardNo)
    {
      var state = StateTestUtil.EmptyState.New(discards: SetupDiscards());

      var returnedCardId = new Identifiable($"{player},{cardNo}");
      state.ReturnFromDiscard(returnedCardId);

      var expectedHands = TestUtil.Sets<ICard>();
      var expectedDiscards = SetupDiscards();
      var returnedCard = expectedDiscards[player].Single(c => c.Equals(returnedCardId));
      expectedDiscards[player].Remove(returnedCard);
      expectedHands[player].Add(returnedCard);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> { new CardReturnedToHand(returnedCard)};
      var expectedState = StateTestUtil.EmptyState.New(hands: expectedHands, discards: expectedDiscards,
        resolvedEffects: expectedResolvedEffects);
      
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ReturnFromDiscards_CardNotPresent()
    {
      var state = StateTestUtil.EmptyState.New(discards: SetupDiscards());

      var returnedCardId = new Identifiable("InvalidId");

      try
      {
        state.ReturnFromDiscard(returnedCardId);
      }
      catch (CardNotPresentException)
      {
        return;
      }
      
      Assert.Fail();
    }
    
    
    static IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> SetupDecks()
    {
      return TestUtil.Stacks(SampleCards(Player.Player1), SampleCards(Player.Player2));
    }

    static IReadOnlyDictionary<Player, IMutableSet<ICard>> SetupDiscards()
    {
      return TestUtil.Sets<ICard>(SampleCards(Player.Player1), SampleCards(Player.Player2));
    }

    static ICard[] SampleCards(Player player)
    {
      return new[]
      {
        (ICard) new SampleActionCard(id: $"{player},0"), new SampleCreatureCard(id: $"{player},1")
      };
    }
  }
}