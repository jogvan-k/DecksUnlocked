using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class ReadyCardsTest
  {
    readonly ReadyCards _sut = new ReadyCards();

    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;

      _sut.Resolve(state);

      var expectedState = StateTestUtil.EmptyState;
      Assert.AreEqual(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_ReadyAndUnreadyCreatures(Player playerTurn)
    {
      var playerCreatureCard1 = new SampleCreatureCard();
      var playerCreatureCard2 = new SampleCreatureCard();
      var opponentCreatureCard1 = new SampleCreatureCard();
      var opponentCreatureCard2 = new SampleCreatureCard();
      var opponentField = new List<Creature>
      {
        new Creature(opponentCreatureCard1, isReady: true),
        new Creature(opponentCreatureCard2, isReady: false)
      };
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {
          playerTurn,
          new List<Creature>
          {
            new Creature(playerCreatureCard1, isReady: true),
            new Creature(playerCreatureCard2, isReady: false)
          }
        },
        {playerTurn.Other(), opponentField}
      };
      var state = StateTestUtil.EmptyMutableState.New(playerTurn: playerTurn, fields: fields);

      _sut.Resolve(state);

      var expectedFields = new Dictionary<Player, IList<Creature>>
      {
        {
          playerTurn, new List<Creature>
          {
            new Creature(playerCreatureCard1, isReady: true),
            new Creature(playerCreatureCard2, isReady: true)
          }
        },
        {playerTurn.Other(), opponentField}
      };
      var expectedState = StateTestUtil.EmptyState.New(playerTurn: playerTurn, fields: expectedFields);
      Assert.AreEqual(expectedState, state);
    }
  }
}