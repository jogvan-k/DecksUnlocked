using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Util
{
    public static class StateTestUtil
    {
        static Metadata _emptyMetadata = EmptyMetadata();

        public static IMutableState EmptyMutableState => new MutableState(
            Player.Player1,
            0,
            false,
            null,
            TestUtil.Ints(),
            TestUtil.Ints(),
            new LazyList<IActionGroup>(),
            TestUtil.Stacks<ICard>(),
            TestUtil.Sets<ICard>(),
            TestUtil.Sets<ICard>(),
            TestUtil.Sets<ICard>(),
            TestUtil.Sets<ICard>(),
            TestUtil.Lists<Creature>(),
            TestUtil.Sets<Artifact>(),
            new LazyStackQueue<IEffect>(),
            new LazyEvents(),
            new LazyList<IResolvedEffect>(),
            new LazyHistoricData(),
            _emptyMetadata);

        static Metadata EmptyMetadata()
        {
            return new(
                Initializers.EmptyDeck(),
                ImmutableLookup<Player, IImmutableSet<House>>.Empty,
                0,
                0);
        }

        public static ImmutableState EmptyState => new(
            Player.Player1,
            0,
            false,
            null,
            TestUtil.Ints().ToReadOnly(),
            TestUtil.Ints().ToReadOnly(),
            new HashSet<IActionGroup>().ToImmutableHashSet(),
            TestUtil.Stacks<ICard>().ToImmutable(),
            TestUtil.Sets<ICard>().ToImmutable(),
            TestUtil.Sets<ICard>().ToImmutable(),
            TestUtil.Sets<ICard>().ToImmutable(),
            TestUtil.Sets<ICard>().ToImmutable(),
            TestUtil.Lists<Creature>().ToImmutable(),
            TestUtil.Sets<Artifact>().ToImmutable(),
            ImmutableArray<IEffect>.Empty,
            new ImmutableEvents(),
            new List<IResolvedEffect>().ToImmutableList(),
            new ImmutableHistoricData(),
            _emptyMetadata);

        public static IMutableState New(
            this IState state,
            Player? playerTurn = null,
            int? turnNumber = null,
            bool isGameOver = false, KeyforgeUnlocked.Types.Lookup<Player, int> keys = null,
            KeyforgeUnlocked.Types.Lookup<Player, int> aember = null,
            House? activeHouse = null,
            IMutableList<IActionGroup> actionGroups = null,
            IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> hands = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> discards = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> archives = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> purgedCards = null,
            IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
            IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts = null,
            IMutableStackQueue<IEffect> effects = null,
            IMutableEvents events = null,
            IMutableList<IResolvedEffect> resolvedEffects = null,
            IMutableHistoricData historicData = null,
            Metadata metadata = null)
        {
            return new MutableState(
                playerTurn ?? state.PlayerTurn,
                turnNumber ?? state.TurnNumber,
                isGameOver || state.IsGameOver,
                activeHouse ?? state.ActiveHouse,
                keys ?? new Dictionary<Player, int>(state.Keys).ToLookup(),
                aember ?? new Dictionary<Player, int>(state.Aember).ToLookup(),
                actionGroups ?? new LazyList<IActionGroup>(state.ActionGroups),
                decks ?? state.Decks.ToMutable(),
                hands ?? state.Hands.ToMutable(),
                discards ?? state.Discards.ToMutable(),
                archives ?? state.Archives.ToMutable(),
                purgedCards ?? state.PurgedCard.ToMutable(),
                fields ?? state.Fields.ToMutable(),
                artifacts ?? state.Artifacts.ToMutable(),
                effects ?? new LazyStackQueue<IEffect>(state.Effects),
                events ?? new LazyEvents(),
                resolvedEffects ?? new LazyList<IResolvedEffect>(state.ResolvedEffects),
                historicData ?? state.HistoricData.ToMutable(),
                metadata ?? state.Metadata);
        }

        /// <summary>
        /// Returns a new state based on the given state.
        /// </summary>
        /// <returns></returns>
        public static IMutableState Extend(
            this IState state,
            Player? playerTurn = null,
            int? turnNumber = null,
            bool isGameOver = false, KeyforgeUnlocked.Types.Lookup<Player, int> keys = null,
            KeyforgeUnlocked.Types.Lookup<Player, int> aember = null,
            House? activeHouse = null,
            IMutableList<IActionGroup> actionGroups = null,
            IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> decks = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> hands = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> discards = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> archives = null,
            IReadOnlyDictionary<Player, IMutableSet<ICard>> purgedCards = null,
            IReadOnlyDictionary<Player, IMutableList<Creature>> fields = null,
            IReadOnlyDictionary<Player, IMutableSet<Artifact>> artifacts = null,
            IMutableList<IResolvedEffect> resolvedEffects = null,
            IMutableHistoricData historicData = null,
            IMutableStackQueue<IEffect> effects = null,
            IMutableEvents events = null,
            Metadata metadata = null)
        {
            return state.New(
                playerTurn,
                turnNumber,
                isGameOver,
                keys,
                aember,
                activeHouse,
                actionGroups,
                decks,
                hands,
                discards,
                archives,
                purgedCards,
                fields,
                artifacts,
                effects,
                events ?? new LazyEvents(),
                resolvedEffects ?? new LazyList<IResolvedEffect>(),
                historicData,
                metadata);
        }

        static Stack<ICard> EmptyDeck => new();

        public static Stack<ICard> SampleDeck =>
            new(Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()));
    }
}