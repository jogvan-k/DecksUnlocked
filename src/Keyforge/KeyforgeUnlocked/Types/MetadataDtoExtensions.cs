using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
    public static class MetadataDtoExtensions
    {
        public static MetadataDto ToDto(this Metadata metadata) => new()
        {
            InitialDecks =
                metadata.InitialDecks.ToDictionary(kv => kv.Key, kv => kv.Value.Select(c => c.ToDto()).ToList()),
            Houses = metadata.Houses.ToDictionary(kv => kv.Key, kv => kv.Value.ToList()),
            TurnCountLimit = metadata.TurnCountLimit,
            RngSeed = metadata.RngSeed
        };

        public static Metadata ToMetadata(this MetadataDto dto) => new(
            dto.InitialDecks.ToReadOnly<Player, List<CardDto>, IImmutableList<ICard>>(kv =>
                ImmutableList.Create(kv.Value.Select(c => c.ToCard()).ToArray())),
            dto.Houses.ToReadOnly<Player, List<House>, IImmutableSet<House>>(kv =>
                ImmutableHashSet.Create<House>(kv.Value.ToArray())),
            dto.TurnCountLimit,
            dto.RngSeed
        );
    }
}