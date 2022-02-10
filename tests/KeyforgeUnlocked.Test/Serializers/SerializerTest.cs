using System;
using System.Collections.Immutable;
using Google.Protobuf.WellKnownTypes;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Serializers;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Serializers
{
    [TestFixture]
    public class SerializerTest
    {
        [Test]
        public void RoundtripTest()
        {
            var serializer = new StateSerializer();
            var originalState = StateTestUtil.EmptyState.New(
                    Player.Player1,
                    2, false,
                    TestUtil.Ints(1, 2),
                    TestUtil.Ints(3, 4),
                    House.Logos,
                    null,
                    TestUtil.Stacks(
                        new ICard[] { new SampleActionCard(House.Logos), new SampleArtifactCard(House.Dis) },
                        new ICard[] { new SampleCreatureCard(House.Mars), new SampleArtifactCard(House.Saurian) }),
                    TestUtil.Sets<ICard>(new SampleCreatureCard(House.Brobnar), new SampleActionCard(House.Saurian)),
                    TestUtil.Sets<ICard>(new SampleActionCard(House.Brobnar), new SampleCreatureCard(House.Saurian)),
                    TestUtil.Sets<ICard>(new SampleArtifactCard(House.Brobnar), new SampleArtifactCard(House.Saurian)),
                    artifacts: TestUtil.Sets<Artifact>(new[]
                    {
                        new Artifact(new SampleArtifactCard(House.Brobnar, new[]
                        {
                            Trait.Elf, Trait.Cyborg
                        })),
                        new Artifact(new SampleArtifactCard(House.Mars, Array.Empty<Trait>()))
                    }, new[] { new Artifact(new SampleArtifactCard()) }),
                    metadata: new Metadata(ImmutableLookup<Player, IImmutableList<ICard>>.Empty,
                        ImmutableLookup<Player, IImmutableSet<House>>.Empty, 10, 20)
                )
                .ToImmutable();

            var serialized = serializer.Serialize(originalState);
            var roundtripState = serializer.Deserialize(serialized);

            StateAsserter.StateEquals(roundtripState, originalState);
        }
    }
}