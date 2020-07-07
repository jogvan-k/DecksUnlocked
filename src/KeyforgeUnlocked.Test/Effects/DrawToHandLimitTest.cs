using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DrawToHandLimitTest
  {
    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_EmptyState(Player player)
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new DrawToHandLimit(player);

      sut.Resolve(state);

      Assert.AreEqual(TestUtil.EmptyMutableState, state);
    }

    [Test]
    public void Resolve_FullDeck([Range(0, 7)] int cardsInHand)
    {
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {
          Player.Player1,
          Enumerable.Range(1, cardsInHand).Select(i => new SimpleCreatureCard(House.Brobnar, i)).ToHashSet<Card>()
        }
      };

      var sampleDeck = TestUtil.SampleDeck;
      var sampleDeckCardCount = sampleDeck.Count;
      var decks = new Dictionary<Player, Stack<Card>> {{Player.Player1, sampleDeck}};
      var state = TestUtil.EmptyMutableState.New(decks: decks, hands: hands);
      var sut = new DrawToHandLimit(Player.Player1);

      sut.Resolve(state);

      var expectedCardDraws = Math.Max(Constants.EndTurnHandLimit - cardsInHand, 0);
      Assert.AreEqual(Math.Max(Constants.EndTurnHandLimit, cardsInHand), state.Hands[Player.Player1].Count);
      Assert.AreEqual(sampleDeckCardCount - expectedCardDraws, state.Decks[Player.Player1].Count);
    }
  }
}