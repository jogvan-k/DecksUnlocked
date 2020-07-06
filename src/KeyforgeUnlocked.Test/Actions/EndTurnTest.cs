using System.Collections.Generic;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class EndTurnTest
  {
    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void DoAction_NextPlayerAndIncreaseTurnNumber(Player playerTurn)
    {
      var turnNumberStart = 1;
      var sut = new EndTurn(TestUtil.EmptyMutableState.New(playerTurn, turnNumberStart));

      var result = sut.DoAction();
      var expectedPlayerTurn = playerTurn == Player.Player1 ? Player.Player2 : Player.Player1;
      Assert.AreEqual(expectedPlayerTurn, result.PlayerTurn);
      Assert.AreEqual(turnNumberStart + 1, result.TurnNumber);
    }
  }
}