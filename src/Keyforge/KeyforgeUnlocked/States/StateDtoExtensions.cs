using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
    public static class StateDtoExtensions
    {
        public static StateDto ToDto(this ImmutableState state) =>
            new()
            {
                PlayerTurn = state.PlayerTurn,
                TurnNumber = state.TurnNumber,
                IsGameOver = state.IsGameOver,
                ActiveHouse = state.ActiveHouse,
                Keys = state.Keys.ToDictionary(),
                Aember = state.Aember.ToDictionary(),
                Decks = state.Decks.ToDictionary(kv => kv.Key, kv => ToDto(kv.Value)),
                Hands = state.Hands.ToDictionary(kv => kv.Key, kv => ToDto(kv.Value)),
                Discards = state.Discards.ToDictionary(kv => kv.Key, kv => ToDto(kv.Value)),
                Archives = state.Archives.ToDictionary(kv => kv.Key, kv => ToDto(kv.Value)),
                PurgedCard = state.PurgedCard.ToDictionary(kv => kv.Key, kv => ToDto(kv.Value)),
                Metadata = state.Metadata.ToDto()
            };

        public static ImmutableState ToImmutableState(this StateDto dto) =>
            new()
            {
                PlayerTurn = dto.PlayerTurn,
                TurnNumber = dto.TurnNumber,
                IsGameOver = dto.IsGameOver,
                ActiveHouse = dto.ActiveHouse,
                Keys = new ImmutableLookup<Player, int>(dto.Keys),
                Aember = new ImmutableLookup<Player, int>(dto.Aember),
                ActionGroups = ImmutableHashSet<IActionGroup>.Empty,
                Decks = dto.Decks.ToReadOnly<Player, List<CardDto>, IImmutableStack<ICard>>(kv => ImmutableStack.Create(ToCard(kv.Value).Reverse().ToArray())),
                Hands = dto.Hands.ToReadOnly<Player, List<CardDto>, IImmutableSet<ICard>>(kv => ImmutableHashSet.Create(ToCard(kv.Value))),
                Discards = dto.Discards.ToReadOnly<Player, List<CardDto>, IImmutableSet<ICard>>(kv => ImmutableHashSet.Create(ToCard(kv.Value))),
                Archives = dto.Archives.ToReadOnly<Player, List<CardDto>, IImmutableSet<ICard>>(kv => ImmutableHashSet.Create(ToCard(kv.Value))),
                PurgedCard = dto.PurgedCard.ToReadOnly<Player, List<CardDto>, IImmutableSet<ICard>>(kv => ImmutableHashSet.Create(ToCard(kv.Value))),
                Fields = new Dictionary<Player, IMutableList<Creature>>
                {
                    {Player.Player1, new LazyList<Creature>()},
                    {Player.Player2, new LazyList<Creature>()}
                }.ToReadOnly().ToImmutable(),
                Artifacts = new Dictionary<Player, IMutableSet<Artifact>>
                {
                    {Player.Player1, new LazySet<Artifact>()},
                    {Player.Player2, new LazySet<Artifact>()}
                }.ToReadOnly().ToImmutable(),
                Effects = ImmutableArray<IEffect>.Empty,
                ResolvedEffects = ImmutableList<IResolvedEffect>.Empty,
                Events = new ImmutableEvents(),
                HistoricData = new ImmutableHistoricData(),
                Metadata = dto.Metadata.ToMetadata()
            };

        static List<CardDto> ToDto(IEnumerable<ICard> cards) => cards.Select(v => v.ToDto()).ToList();
        static ICard[] ToCard(IEnumerable<CardDto> dtos) => dtos.Select(c => c.ToCard()).ToArray();
    }
}