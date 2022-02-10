using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
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
      var decks = SetupDecks();
      var initialDecks = decks.ToReadOnly(kv => (IImmutableList<ICard>) kv.Value.ToImmutableList());
      var metadata = new Metadata(initialDecks, ImmutableLookup<Player, IImmutableSet<House>>.Empty, 0, -1);
      var state = StateTestUtil.EmptyState.New(decks: decks, metadata: metadata);

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
        resolvedEffects: expectedResolvedEffects, metadata: metadata);
      Assert.That(cardsDrawn, Is.EqualTo(expectedCardsDrawn));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ReturnFromDiscards(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 1)] int cardNo)
    {
      var state = StateTestUtil.EmptyState.New(discards: SetupCardSets());
      var returnedCardId = new Identifiable($"{player},{cardNo}");
      
      state.ReturnFromDiscard(returnedCardId);

      var expectedHands = TestUtil.Sets<ICard>();
      var expectedDiscards = SetupCardSets();
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
      var state = StateTestUtil.EmptyState.New(discards: SetupCardSets());

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

    [Test]
    public void PurgeCard(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var state = StateTestUtil.EmptyMutableState;
      var card = new SampleActionCard();

      state.PurgeCard(player, card);

      var expectedPurgedCards = TestUtil.Sets<ICard>();
      expectedPurgedCards[player].Add(card);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> { new CardPurged(card)};
      var expectedState = StateTestUtil.EmptyState.New(purgedCards: expectedPurgedCards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void PurgeFromDiscards(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 1)] int cardNo)
    {
      var state = StateTestUtil.EmptyState.New(discards: SetupCardSets());
      var purgedCardId = new Identifiable($"{player},{cardNo}");
      
      state.PurgeFromDiscard(purgedCardId);
      
      var expectedDiscards = SetupCardSets();
      var expectedPurgedCards = TestUtil.Sets<ICard>();
      var purgedCard = expectedDiscards[player].Single(c => c.Equals(purgedCardId));
      expectedDiscards[player].Remove(purgedCard);
      expectedPurgedCards[player].Add(purgedCard);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> { new CardPurged(purgedCard)};
      var expectedState = StateTestUtil.EmptyState.New(discards: expectedDiscards, purgedCards: expectedPurgedCards, resolvedEffects: expectedResolvedEffects);
      
      StateAsserter.StateEquals(expectedState, state);
    }

    
    [Test]
    public void PurgeFromDiscards_CardNotPresent()
    {
      var state = StateTestUtil.EmptyState.New(discards: SetupCardSets());
      var purgedCardId = new Identifiable("InvalidId");

      try
      {
        state.PurgeFromDiscard(purgedCardId);
      }
      catch (CardNotPresentException)
      {
        return;
      }
      
      Assert.Fail();
    }
    
    [Test]
    public void ArchiveFromHand(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 1)] int cardNo)
    {
      var state = StateTestUtil.EmptyState.New(hands: SetupCardSets());
      
      var returnedCardId = new Identifiable($"{player},{cardNo}");
      state.ArchiveFromHand(returnedCardId);
      
      var expectedHands = SetupCardSets();
      var archivedCard = SampleCards(player)[cardNo];
      expectedHands[player].Remove(archivedCard);
      var expectedArchives = TestUtil.Sets<ICard>();
      expectedArchives[player].Add(archivedCard);
      var resolvedEffects = new LazyList<IResolvedEffect> { new CardArchived(archivedCard)};
      
      StateAsserter.StateEquals(StateTestUtil.EmptyState.New(hands: expectedHands, archives: expectedArchives, resolvedEffects: resolvedEffects), state);
    }
    
    
    [Test]
    public void ArchiveFromHand_CardNotPresent()
    {
      var state = StateTestUtil.EmptyState.New(hands: SetupCardSets());

      var returnedCardId = new Identifiable("InvalidId");

      try
      {
        state.ArchiveFromHand(returnedCardId);
      }
      catch (CardNotPresentException)
      {
        return;
      }
      
      Assert.Fail();
    }

    [Test]
    public void PopArchive_NoCardsInArchive_NoEffect([Values(Player.Player1, Player.Player2)] Player playerTurn)
    {
      var archivedCard = new SampleActionCard();
      var archives = TestUtil.Sets<ICard>();
      archives[playerTurn.Other()].Add(archivedCard);
      var state = StateTestUtil.EmptyState.New(playerTurn: playerTurn, archives: archives,hands: SetupCardSets());

      state.PopArchive();

      var expectedArchives = TestUtil.Sets<ICard>();
      expectedArchives[playerTurn.Other()].Add(archivedCard);
      var expectedState = StateTestUtil.EmptyState.New(playerTurn: playerTurn, archives: expectedArchives, hands: SetupCardSets());
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void PopArchive([Values(Player.Player1, Player.Player2)]Player playerTurn)
    {
      var state = StateTestUtil.EmptyState.New(playerTurn: playerTurn, archives: SetupCardSets());
      
      state.PopArchive();

      var expectedArchives = SetupCardSets();
      var poppedArchive = expectedArchives[playerTurn].ToArray();
      var expectedHands = TestUtil.Sets<ICard>();
      poppedArchive.Select(c =>
      {
        expectedArchives[playerTurn].Remove(c);
        expectedHands[playerTurn].Add(c);
        return true;
      }).ToList();
      var expectedResolvedEffects = new LazyList<IResolvedEffect> { new ArchivedClaimed()};
      
      StateAsserter.StateEquals(StateTestUtil.EmptyState.New(playerTurn: playerTurn, archives: expectedArchives, hands: expectedHands, resolvedEffects: expectedResolvedEffects), state);
    }

    [Test]
    public void DiscardTopCardOfDeck_EmptyDeck(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var state = StateTestUtil.EmptyMutableState.New(playerTurn: player);

      var discardedCard = state.DiscardTopOfDeck();
      
      Assert.Null(discardedCard);
    }

    [Test]
    public void DiscardTopCardOfDeck(
      [Values(Player.Player1, Player.Player2)]
      Player player)
    {
      var state = StateTestUtil.EmptyMutableState.New(playerTurn: player, decks: SetupDecks());

      var discardedCard = state.DiscardTopOfDeck();

      var expectedDecks = SetupDecks();
      var expectedDiscard = expectedDecks[player].Dequeue();
      Assert.That(discardedCard, Is.EqualTo(expectedDiscard));
      var expectedDiscards = TestUtil.Sets<ICard>();
      expectedDiscards[player].Add(expectedDiscard);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new CardDiscarded(expectedDiscard)};
      var expectedState = StateTestUtil.EmptyState.New(playerTurn: player, decks: expectedDecks, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ShuffleDiscardsIntoDeck_NoCardsInDeckOrDiscards(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      Assert.False(StateTestUtil.EmptyMutableState.ShuffleDiscardsIntoDeck(player));
    }

    [Test]
    public void ShuffleDiscardsIntoDeck(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var discards = SetupCardSets();
      var decks = discards.ToImmutableDictionary(kv => kv.Key, kv => new Deck(kv.Value.OrderBy(v => v.Id)));
      var initialDecks = decks.ToReadOnly(kv => (IImmutableList<ICard>) kv.Value.Cards);
      var metadata = new Metadata(initialDecks, ImmutableLookup<Player, IImmutableSet<House>>.Empty, 0, -1);

      var state = StateTestUtil.EmptyState.New(player, discards: discards, metadata: metadata);
      
      var result = state.ShuffleDiscardsIntoDeck(player);

      var expectedDiscards = SetupCardSets();
      expectedDiscards[player].Clear();
      var expectedDecks = SetupDecks();
      expectedDecks[player.Other()].Clear();
      var expectedHistoricData = new LazyHistoricData();
      ((IMutableHistoricData) expectedHistoricData).NumberOfShuffles[player] = 1;
      var expectedResolvedEffects = new LazyList<IResolvedEffect>(new []{(IResolvedEffect) new DiscardShuffledIntoDeck(player)});
      var expectedState = StateTestUtil.EmptyState.New(player, decks: expectedDecks, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects,  historicData: expectedHistoricData, metadata: metadata);
      foreach (var card in state.Decks[player])
      {
        Console.WriteLine(card.Id);
      }
      
      Assert.True(result);
      StateAsserter.StateEquals(expectedState, state);
    }
    
    static IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> SetupDecks()
    {
      return TestUtil.Stacks(SampleCards(Player.Player1), SampleCards(Player.Player2));
    }

    static IReadOnlyDictionary<Player, IMutableSet<ICard>> SetupCardSets()
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