using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Actions
{
    [TestFixture]
    sealed class UseArtifactTest : ActionTestBase<UseArtifact>
    {
        House ActiveHouse = House.Dis;

        [Test]
        public void UseArtifact()
        {
            var actionResolved = false;
            Callback actionAbility = (_, _, _) => actionResolved = true;
            var artifactCard = new SampleArtifactCard(ActiveHouse, actionAbility: actionAbility);
            var artifact = new Artifact(artifactCard, true);
            var state = Setup(artifact);
            var sut = new UseArtifact(state.ToImmutable(), artifact, false);

            var expectedState = Setup(new Artifact(artifactCard));
            expectedState.HistoricData.ActionPlayedThisTurn = true;
            expectedState.ResolvedEffects.Add(new ArtifactUsed(artifact));
            ActAndAssert(sut, state, expectedState);
            Assert.True(actionResolved);
        }

        [Test]
        public void UseArtifact_OutsideActiveHouse_Allowed()
        {
            var actionResolved = false;
            Callback actionAbility = (_, _, _) => actionResolved = true;
            var artifactCard = new SampleArtifactCard(House.Brobnar, actionAbility: actionAbility);
            var artifact = new Artifact(artifactCard, true);
            var state = Setup(artifact);
            var sut = new UseArtifact(state.ToImmutable(), artifact, true);

            var expectedState = Setup(new Artifact(artifactCard));
            expectedState.HistoricData.ActionPlayedThisTurn = true;
            expectedState.ResolvedEffects.Add(new ArtifactUsed(artifact));
            ActAndAssert(sut, state, expectedState);
            Assert.True(actionResolved);
        }

        [Test]
        public void UseArtifact_OutsideActiveHouse()
        {
            Callback actionAbility = (_, _, _) => { };
            var artifactCard = new SampleArtifactCard(House.Logos, actionAbility: actionAbility);
            var artifact = new Artifact(artifactCard, true);
            var state = Setup(artifact);
            var sut = new UseArtifact(state.ToImmutable(), artifact, false);

            System.Action<NotFromActiveHouseException> asserts = e => e.Equals(artifact);

            ActExpectException(sut, state, asserts);
        }

        [Test]
        public void UseArtifact_ArtifactNotReady()
        {
            Callback actionAbility = (_, _, _) => { };
            var artifactCard = new SampleArtifactCard(ActiveHouse, actionAbility: actionAbility);
            var artifact = new Artifact(artifactCard);
            var state = Setup(artifact);
            var sut = new UseArtifact(state.ToImmutable(), artifact, false);

            System.Action<ArtifactNotReadyException> asserts = e => e.Equals(artifact);

            ActExpectException(sut, state, asserts);
        }

        [Test]
        public void UseArtifact_NoActionAbility()
        {
            var artifactCard = new SampleArtifactCard(ActiveHouse);
            var artifact = new Artifact(artifactCard, true);
            var state = Setup(artifact);
            var sut = new UseArtifact(state.ToImmutable(), artifact, false);

            System.Action<NoCallbackException> asserts = e => { e.Id.Equals(artifact); };
            ActExpectException(sut, state, asserts);
        }

        IMutableState Setup(Artifact artifact)
        {
            return StateTestUtil.EmptyState.New(activeHouse: ActiveHouse, artifacts: TestUtil.Sets(artifact));
        }
    }
}