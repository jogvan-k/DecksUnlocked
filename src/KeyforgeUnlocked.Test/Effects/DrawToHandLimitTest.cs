using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DrawToHandLimitTest
  {
    static Card[] sampleCards =
    {
      new SimpleCreatureCard(), new SimpleCreatureCard(), new SimpleCreatureCard(), new SimpleCreatureCard(),
      new SimpleCreatureCard(), new SimpleCreatureCard(), new SimpleCreatureCard(), new SimpleCreatureCard(),
    };

    [Test]
    public void Resolve_EmptyState()
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new DrawToHandLimit();

      sut.Resolve(state);

      Assert.AreEqual(TestUtil.EmptyMutableState, state);
    }

    [Test]
    public void Resolve_FullDeck([Range(0, 7)] int cardsInHand)
    {
      var hands = StateWithCardsInHand(cardsInHand);

      var sampleDeck = SampleSets.SampleDeck;
      var sampleDeckCardCount = sampleDeck.Count;
      var decks = InitializeDeck();
      var state = TestUtil.EmptyMutableState.New(decks: decks, hands: hands);
      var sut = new DrawToHandLimit();

      sut.Resolve(state);

      var expectedCardsInHand = Math.Max(6, cardsInHand);
      var expectedDraws = expectedCardsInHand - cardsInHand;
      var expectedHands = StateWithCardsInHand(cardsInHand);
      var expectedDecks = InitializeDeck();
      for (var i = 0; i < expectedDraws; i++)
        expectedHands[Player.Player1].Add(expectedDecks[Player.Player1].Pop());
      var expectedState = TestUtil.EmptyMutableState.New(decks: expectedDecks, hands: expectedHands);
      if (expectedDraws > 0)
        expectedState.ResolvedEffects.Add(new CardsDrawn(expectedDraws));
      Assert.AreEqual(expectedState, state);
    }

    static Dictionary<Player, Stack<Card>> InitializeDeck()
    {
      return new Dictionary<Player, Stack<Card>>
        {{Player.Player1, SampleSets.SampleDeck}, {Player.Player2, new Stack<Card>()}};
    }

    static Dictionary<Player, ISet<Card>> StateWithCardsInHand(int cardsInHand)
    {
      return new Dictionary<Player, ISet<Card>>
      {
        {
          Player.Player1,
          new HashSet<Card>(sampleCards[new Range(0, cardsInHand)])
        }
      };
    }
  }
}