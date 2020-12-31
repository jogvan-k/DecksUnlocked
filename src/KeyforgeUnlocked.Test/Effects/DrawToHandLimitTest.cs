using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DrawToHandLimitTest
  {
    static readonly ICard[] sampleCards = Enumerable.Range(0, 8).Select(c => new SampleCreatureCard()).ToArray();

    readonly DrawToHandLimit _sut = new DrawToHandLimit();
    
    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new DrawToHandLimit();

      sut.Resolve(state);

      Assert.AreEqual(StateTestUtil.EmptyMutableState, state);
    }

    [Test]
    public void Resolve_HandLimitModified([Values(-5, -1, 1, 5)] int modifiedAmount)
    {
      var hands = StateWithCardsInHand(4);
      var decks = InitializeDeck();
      var events = new LazyEvents();
      events.Subscribe(new Identifiable(""), ModifierType.HandLimit, _ => modifiedAmount);
      var state = StateTestUtil.EmptyMutableState.New(decks: decks, hands: hands, events: events);

      _sut.Resolve(state);

      var expectedCardsInHand = Math.Max(6 + modifiedAmount, 4);
      var expectedDraws = expectedCardsInHand - 4;
      var expectedState = ExpectedState(4 ,expectedDraws);
      expectedState.Events = events;
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_FullDeck([Range(0, 7)] int cardsInHand)
    {
      var hands = StateWithCardsInHand(cardsInHand);
      var decks = InitializeDeck();
      var state = StateTestUtil.EmptyMutableState.New(decks: decks, hands: hands);

      _sut.Resolve(state);

      var expectedCardsInHand = Math.Max(6, cardsInHand);
      var expectedDraws = expectedCardsInHand - cardsInHand;
      var expectedState = ExpectedState(cardsInHand, expectedDraws);
      StateAsserter.StateEquals(expectedState, state);
    }

    IMutableState ExpectedState(int startCardsInHand, int expectedDraws)
    {
      var expectedHands = StateWithCardsInHand(startCardsInHand);
      var expectedDecks = InitializeDeck();
      for (var i = 0; i < expectedDraws; i++)
        expectedHands[Player.Player1].Add(expectedDecks[Player.Player1].Dequeue());
      var expectedState = StateTestUtil.EmptyMutableState.New(decks: expectedDecks, hands: expectedHands);
      if (expectedDraws > 0)
        expectedState.ResolvedEffects.Add(new CardsDrawn(Player.Player1, expectedDraws));
      return expectedState;
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