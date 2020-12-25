using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DrawToHandLimitTest
  {
    static readonly ICard[] sampleCards = Enumerable.Range(0, 8).Select(c => new SampleCreatureCard()).ToArray();

    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new DrawToHandLimit();

      sut.Resolve(state);

      Assert.AreEqual(StateTestUtil.EmptyMutableState, state);
    }

    [Test]
    public void Resolve_FullDeck([Range(0, 7)] int cardsInHand)
    {
      var hands = StateWithCardsInHand(cardsInHand);

      var sampleDeck = SampleSets.SampleDeck;
      var decks = InitializeDeck();
      var state = StateTestUtil.EmptyMutableState.New(decks: decks, hands: hands);
      var sut = new DrawToHandLimit();

      sut.Resolve(state);

      var expectedCardsInHand = Math.Max(6, cardsInHand);
      var expectedDraws = expectedCardsInHand - cardsInHand;
      var expectedHands = StateWithCardsInHand(cardsInHand);
      var expectedDecks = InitializeDeck();
      for (var i = 0; i < expectedDraws; i++)
        expectedHands[Player.Player1].Add(expectedDecks[Player.Player1].Dequeue());
      var expectedState = StateTestUtil.EmptyMutableState.New(decks: expectedDecks, hands: expectedHands);
      if (expectedDraws > 0)
        expectedState.ResolvedEffects.Add(new CardsDrawn(Player.Player1, expectedDraws));
      StateAsserter.StateEquals(expectedState, state);
    }

    static IImmutableDictionary<Player, IMutableStackQueue<ICard>> InitializeDeck()
    {
      return TestUtil.Stacks(SampleSets.SampleDeck).ToImmutableDictionary();
    }

    static IImmutableDictionary<Player, IMutableSet<ICard>> StateWithCardsInHand(int cardsInHand)
    {
      return new Dictionary<Player, IMutableSet<ICard>>
      {
        {
          Player.Player1,
          new LazySet<ICard>(sampleCards[new Range(0, cardsInHand)])
        }
      }.ToImmutableDictionary();
    }
  }
}