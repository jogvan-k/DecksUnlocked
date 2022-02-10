using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
    [TestFixture]
    sealed class UseArtifactGroupTest
    {
        static House ActiveHouse = House.Untamed;

        static readonly ImmutableState _immutableState =
            StateTestUtil.EmptyState.New(activeHouse: ActiveHouse).ToImmutable();

        [Test]
        public void ArtifactNotReady_NoActions()
        {
            var sut = new UseArtifactGroup(
                new Artifact(new SampleArtifactCard(ActiveHouse, actionAbility: Delegates.NoChange)));

            var result = sut.Actions(_immutableState);

            Assert.IsEmpty(result);
        }

        [Test]
        public void ArtifactNotFromActiveHouse_NoActions()
        {
            var sut = new UseArtifactGroup(
                new Artifact(new SampleArtifactCard(House.Dis, actionAbility: Delegates.NoChange), true));

            var result = sut.Actions(_immutableState);

            Assert.IsEmpty(result);
        }

        [Test]
        public void ArtifactHasNoActivationAbility_NoActions()
        {
            var sut = new UseArtifactGroup(
                new Artifact(new SampleArtifactCard(ActiveHouse), true));

            var result = sut.Actions(_immutableState);

            Assert.IsEmpty(result);
        }

        [Test]
        public void ArtifactHasActivationAbilityAndIsReadyAndIsInActiveHouse_Action()
        {
            var artifact = new Artifact(new SampleArtifactCard(ActiveHouse, actionAbility: Delegates.NoChange), true);
            var sut = new UseArtifactGroup(artifact);

            var result = sut.Actions(_immutableState);

            var action = (UseArtifact)result.Single();
            Assert.That(action.Artifact, Is.EqualTo(artifact));
            Assert.False(action.AllowOutOfHouseUse);
        }

        [Test]
        public void ArtifactHasActivationAbilityAndIsReadyAndIsNotActiveHouse_Action()
        {
            var sampleArtifactCard = new SampleArtifactCard(House.Dis, actionAbility: Delegates.NoChange);
            var artifact = new Artifact(sampleArtifactCard, true);
            var sut = new UseArtifactGroup(artifact, true);

            var result = sut.Actions(_immutableState);

            var action = (UseArtifact)result.Single();
            Assert.That(action.Artifact, Is.EqualTo(artifact));
            Assert.True(action.AllowOutOfHouseUse);
        }
    }
}