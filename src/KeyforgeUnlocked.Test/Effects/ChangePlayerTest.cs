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

      var expectedState = TestUtil.EmptyMutableState.New(playerTurn.Other(), turnNumberStart + 1);
      Assert.AreEqual(expectedState, state);
    }
  }
}