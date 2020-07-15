using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using static KeyforgeUnlockedTest.Util.CreatureTestUtil;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class ReadyCardsTest
  {
    readonly ReadyCards _sut = new ReadyCards();

    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateUtil.EmptyMutableState;

      _sut.Resolve(state);

      var expectedState = StateUtil.EmptyState;
      Assert.AreEqual(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_ReadyAndUnreadyCreatures(Player playerTurn)
    {
      var opponentField = new List<Creature>
        {SampleCreature("3", true), SampleCreature("4", false)};
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {
          playerTurn,
          new List<Creature>
          {
            SampleCreature("1", true),
            SampleCreature("2", false)
          }
        },
        {playerTurn.Other(), opponentField}
      };
      var state = StateUtil.EmptyMutableState.New(playerTurn: playerTurn, fields: fields);

      _sut.Resolve(state);

      var expectedFields = new Dictionary<Player, IList<Creature>>
      {
        {playerTurn, new List<Creature> {SampleCreature("1", true), SampleCreature("2", true)}},
        {playerTurn.Other(), opponentField}
      };
      var expectedState = StateUtil.EmptyState.New(playerTurn: playerTurn, fields: expectedFields);
      Assert.AreEqual(expectedState, state);
    }
  }
}