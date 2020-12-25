using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
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
      [Values(Player.Player1, Player.Player2)] Player player,
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
      if(expectedCardsDrawn > 0)
        expectedResolvedEffects.Add(new CardsDrawn(player, expectedCardsDrawn));
      var expectedState = StateTestUtil.EmptyState.New(decks: expectedDecks, hands: expectedHands, resolvedEffects: expectedResolvedEffects);
      Assert.That(cardsDrawn, Is.EqualTo(expectedCardsDrawn));
      StateAsserter.StateEquals(expectedState, state);
    }

    static IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> SetupDecks()
    {
      return TestUtil.Stacks(
        new[]
        {
          (ICard) new SampleActionCard(id: $"{Player.Player1}1"), new SampleCreatureCard(id: $"{Player.Player1}2")
        },
        new[]
        {
          (ICard) new SampleActionCard(id: $"{Player.Player2}1"), new SampleCreatureCard(id: $"{Player.Player2}2")
        });
    }
  }
}