using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using NUnit.Framework;
using UnlockedCore.States;
using EndTurn = KeyforgeUnlocked.Effects.EndTurn;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  public class EndTurnTest
  {
    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_NextPlayerAndIncreaseTurnNumber(Player playerTurn)
    {
      var turnNumberStart = 1;
      var state = TestUtil.EmptyMutableState.New(playerTurn, turnNumberStart);
      var sut = new EndTurn();

      sut.Resolve(state);

      var expectedState = TestUtil.EmptyMutableState.New(
        playerTurn.Other(),
        turnNumberStart + 1,
        resolvedEffects: new List<IResolvedEffect> {new KeyforgeUnlocked.ResolvedEffects.TurnEnded()});
      Assert.AreEqual(expectedState, state);
    }
  }
}