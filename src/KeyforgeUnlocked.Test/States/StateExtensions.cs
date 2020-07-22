using System;
using System.Collections.Generic;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class StateExtensions
  {
    [Test]
    public void Steal_FixedAvailableAmber([Range(0, 3)] int stealingAmount)
    {
      var aember = TestUtil.Ints(0, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);

      state.Steal( stealingAmount);

      var expectedStolen = Math.Min(stealingAmount, 2);
      var expectedAember = TestUtil.Ints(expectedStolen, 2 - expectedStolen);
      var expectedResolvedEffects = new List<IResolvedEffect>();
      if(expectedStolen > 0)
        expectedResolvedEffects.Add(new AemberStolen(Player.Player1, expectedStolen));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}