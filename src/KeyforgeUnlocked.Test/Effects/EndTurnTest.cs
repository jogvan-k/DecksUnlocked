using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using DeclareHouse = KeyforgeUnlocked.Effects.DeclareHouse;
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
      var state = StateTestUtil.EmptyMutableState.New(playerTurn, turnNumberStart, activeHouse: House.Brobnar);
      var sut = new EndTurn();

      sut.Resolve(state);

      var expectedEffects = new StackQueue<IEffect>(new[] {(IEffect) new DeclareHouse(), new TryForge()});
      var expectedState = StateTestUtil.EmptyMutableState.New(
        playerTurn.Other(),
        turnNumberStart + 1,
        resolvedEffects: new List<IResolvedEffect> {new TurnEnded()},
        effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}