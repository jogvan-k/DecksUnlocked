using System.Collections.Generic;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class InitiateGameTest
  {
    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new InitiateGame();

      sut.Resolve(state);

      var expectedEffects =
        new LazyStackQueue<IEffect>(new[] {(IEffect) new EndTurn(), new DrawToHandLimit(), new ReadyCardsAndRestoreArmor(), new FirstTurn(), new DeclareHouse(), new DrawInitialHands()});
      var expectedHistoricData = new LazyHistoricData();
      ((IMutableHistoricData) expectedHistoricData).NumberOfShuffles = new Lookup<Player, int>(
        new Dictionary<Player, int>
        {
          {Player.Player1, 1},
          {Player.Player2, 1}
        });
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects, historicData: expectedHistoricData);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}