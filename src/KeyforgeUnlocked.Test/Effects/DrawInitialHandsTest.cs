using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnlockedCore.States;
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
      var startDecks = new Dictionary<Player, Stack<Card>> {{Player.Player1, startDeck1}, {Player.Player2, startDeck2}};
      var state = StateTestUtil.EmptyState.New(decks: startDecks);
      var sut = new DrawInitialHands();

      sut.Resolve(state);

      var expectedDecks = new Dictionary<Player, Stack<Card>>
        {{Player.Player1, expectedDeck1}, {Player.Player2, expectedDeck2}};
      var expectedHands = new Dictionary<Player, ISet<Card>>
        {{Player.Player1, expectedHand1}, {Player.Player2, expectedHand2}};
      var expectedState = StateTestUtil.EmptyState.New(decks: expectedDecks,hands:expectedHands);
      StateAsserter.StateEquals(expectedState, state);
    }

    static Stack<Card> InitializeDeck(int expectedDraws,
      out Stack<Card> expectedDeck,
      out ISet<Card> expectedHand)
    {
      var startDeck = SampleSets.SampleDeck.ToArray();
      var finishDeck = new Card[startDeck.Length - expectedDraws];
      var finishHand = new Card[expectedDraws];
//      var finishDeck = startDeck.Take(startDeck.Count - expectedDraws);
      Array.Copy(startDeck, 0, finishDeck, 0, startDeck.Length - expectedDraws);
      Array.Copy(startDeck, startDeck.Length - expectedDraws, finishHand, 0, expectedDraws);
      expectedDeck = new Stack<Card>(finishDeck);
      expectedHand = new HashSet<Card>(finishHand);
      return new Stack<Card>(startDeck);
    }
  }
}