using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DrawInitialHandsTest
  {
    [Test]
    public void Resolve()
    {
      var startDeck1 = InitializeDeck(FirstPlayerStartHand, out var expectedDeck1, out var expectedHand1);
      var startDeck2 = InitializeDeck(SecondPlayerStartHand, out var expectedDeck2, out var expectedHand2);
      var startDecks = new Dictionary<Player, IMutableStackQueue<ICard>> {{Player.Player1, startDeck1}, {Player.Player2, startDeck2}}.ToImmutableDictionary();
      var state = StateTestUtil.EmptyState.New(decks: startDecks);
      var sut = new DrawInitialHands();

      sut.Resolve(state);

      var expectedDecks = new Dictionary<Player, IMutableStackQueue<ICard>>
        {{Player.Player1, expectedDeck1}, {Player.Player2, expectedDeck2}}.ToImmutableDictionary();
      var expectedHands = new Dictionary<Player, IMutableSet<ICard>>
        {{Player.Player1, expectedHand1}, {Player.Player2, expectedHand2}}.ToImmutableDictionary();
      var expectedResolvedEffects = new LazyList<IResolvedEffect> { new CardsDrawn(Player.Player1, FirstPlayerStartHand), new CardsDrawn(Player.Player2, SecondPlayerStartHand)};
      var expectedState = StateTestUtil.EmptyState.New(decks: expectedDecks, hands:expectedHands, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    static IMutableStackQueue<ICard> InitializeDeck(int expectedDraws,
      out IMutableStackQueue<ICard> expectedDeck,
      out IMutableSet<ICard> expectedHand)
    {
      var startDeck = SampleSets.SampleDeck.ToArray();
      var finishDeck = new Card[startDeck.Length - expectedDraws];
      var finishHand = new Card[expectedDraws];
      Array.Copy(startDeck, 0, finishDeck, 0, startDeck.Length - expectedDraws);
      Array.Copy(startDeck, startDeck.Length - expectedDraws, finishHand, 0, expectedDraws);
      expectedDeck = new LazyStackQueue<ICard>(finishDeck);
      expectedHand = new LazySet<ICard>(finishHand);
      return new LazyStackQueue<ICard>(startDeck);
    }
  }
}