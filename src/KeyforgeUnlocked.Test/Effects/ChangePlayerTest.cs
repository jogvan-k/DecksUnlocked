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
      var sut = new ChangePlayer(playerTurn);

      sut.Resolve(state);

      var expectedPlayerTurn = playerTurn == Player.Player1 ? Player.Player2 : Player.Player1;
      Assert.AreEqual(expectedPlayerTurn, state.PlayerTurn);
      Assert.AreEqual(turnNumberStart + 1, state.TurnNumber);
    }
  }
}