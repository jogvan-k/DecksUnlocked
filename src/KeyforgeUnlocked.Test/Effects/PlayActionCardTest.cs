using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class PlayActionCardTest
  {

    [Test]
    public void Resolve()
    {
      var playAbilityResolved = false;
      var actionCard = new SampleActionCard(playAbility: (s, i) => playAbilityResolved = true);
      var sut = new PlayActionCard(actionCard);
      var state = StateTestUtil.EmptyMutableState;

      sut.Resolve(state);

      var expectedDiscards = TestUtil.Sets<Card>(actionCard);
      var expectedResolvedEffects = new List<IResolvedEffect>{new ActionPlayed(actionCard)};
      var expectedState = StateTestUtil.EmptyState.New(discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(playAbilityResolved);
    }
  }
}