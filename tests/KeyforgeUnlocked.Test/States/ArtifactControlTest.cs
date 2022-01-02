using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class ArtifactControlTest
  {
    static readonly IArtifactCard _sampleArtifactCard = new SampleArtifactCard();
    static readonly IArtifactCard _otherArtifactCard = new SampleArtifactCard();

    [Test]
    public void DestroyArtifact_ArtifactNotPresent()
    {
      var state = StateTestUtil.EmptyMutableState;

      try
      {
        state.DestroyArtifact(_sampleArtifactCard);
      }
      catch (ArtifactNotPresentException e)
      {
        Assert.That(e.Id, Is.EqualTo(_sampleArtifactCard));
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void DestroyArtifact(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var state = StateTestUtil.EmptyMutableState.New(player, artifacts: SetupArtifacts(player, out _));

      state.DestroyArtifact(_sampleArtifactCard);

      var expectedArtifacts = SetupArtifacts(player, out var artifact);
      expectedArtifacts[player].Remove(artifact);
      var expectedDiscards = TestUtil.Sets<ICard>();
      expectedDiscards[player].Add(_sampleArtifactCard);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new ArtifactDestroyed(artifact)};
      var expectedState = StateTestUtil.EmptyState.New(player, artifacts: expectedArtifacts, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    public ImmutableLookup<Player, IMutableSet<Artifact>> SetupArtifacts(Player player, out Artifact artifact)
    {
      var artifacts = TestUtil.Sets<Artifact>();
      artifact = new Artifact(_sampleArtifactCard);
      artifacts[player].Add(artifact);
      artifacts[player].Add(new Artifact(_otherArtifactCard));
      return artifacts;
    }
  }
}