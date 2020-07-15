using System.Collections.Generic;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  public class TryForgeTest
  {
    readonly TryForge _sut = new TryForge();

    [Test]
    public void Resolve_WithAember([Range(0, 7)] int aember)
    {
      var startAember = new Dictionary<Player, int> {{Player.Player1, aember}, {Player.Player2, aember}};
      var state = StateTestUtil.EmptyState.New(aember: startAember);

      _sut.Resolve(state);

      IState expectedState;
      if (aember < DefaultForgeCost)
      {
        expectedState = StateTestUtil.EmptyState.New(aember: startAember);
      }
      else
      {
        var expectedKeys = new Dictionary<Player, int> {{Player.Player1, 1}, {Player.Player2, 0}};
        var expectedAembers = new Dictionary<Player, int>
          {{Player.Player1, aember - DefaultForgeCost}, {Player.Player2, aember}};
        var expectedResolvedEffects = new List<IResolvedEffect> {new KeyForged(DefaultForgeCost)};
        expectedState = StateTestUtil.EmptyState.New(
          keys: expectedKeys, aember: expectedAembers, resolvedEffects: expectedResolvedEffects);
      }

      Assert.AreEqual(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_ForgeToWinGame(Player winningPlayer)
    {
      var startKeys = new Dictionary<Player, int>
      {
        {winningPlayer, KeysRequiredToWin - 1},
        {winningPlayer.Other(), 0}
      };
      var startAember = new Dictionary<Player, int>
      {
        {winningPlayer, DefaultForgeCost},
        {winningPlayer.Other(), 0}
      };
      var state = StateTestUtil.EmptyState.New(playerTurn: winningPlayer, keys: startKeys, aember: startAember);

      _sut.Resolve(state);

      var expectedKeys = new Dictionary<Player, int> {{winningPlayer, KeysRequiredToWin}, {winningPlayer.Other(), 0}};
      var expectedResolvedEffects = new List<IResolvedEffect>{new KeyForged(DefaultForgeCost)};
      var expectedState = StateTestUtil.EmptyState.New(playerTurn: winningPlayer, keys: expectedKeys, isGameOver: true, resolvedEffects: expectedResolvedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}