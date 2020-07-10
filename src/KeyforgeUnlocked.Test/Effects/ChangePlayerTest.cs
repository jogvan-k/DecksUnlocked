using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  public class ChangePlayerTest
  {
    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_NextPlayerAndIncreaseTurnNumber(Player playerTurn)
    {
      var turnNumberStart = 1;
      var state = TestUtil.EmptyMutableState.New(playerTurn, turnNumberStart);
      var sut = new ChangePlayer();

      sut.Resolve(state);

      var expectedPlayerTurn = playerTurn.Other();
      Assert.AreEqual(expectedPlayerTurn, state.PlayerTurn);
      Assert.AreEqual(turnNumberStart + 1, state.TurnNumber);
    }
  }
}