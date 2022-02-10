using System.Collections.Generic;
using System.Collections.Immutable;
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
    public interface IState : ICoreState
    {
        bool IsGameOver { get; }
        House? ActiveHouse { get; }
        ImmutableLookup<Player, int> Keys { get; }
        ImmutableLookup<Player, int> Aember { get; }
        IImmutableSet<IActionGroup> ActionGroups { get; }
        IReadOnlyDictionary<Player, IImmutableStack<ICard>> Decks { get; }
        IReadOnlyDictionary<Player, IImmutableSet<ICard>> Hands { get; }
        IReadOnlyDictionary<Player, IImmutableSet<ICard>> Discards { get; }
        IReadOnlyDictionary<Player, IImmutableSet<ICard>> Archives { get; }
        IReadOnlyDictionary<Player, IImmutableSet<ICard>> PurgedCard { get; }
        IReadOnlyDictionary<Player, IImmutableList<Creature>> Fields { get; }
        IReadOnlyDictionary<Player, IImmutableSet<Artifact>> Artifacts { get; }
        ImmutableArray<IEffect> Effects { get; }
        ImmutableEvents Events { get; }

        /// <summary>
        /// Effects that have been resolved since the last action was resolved./>
        /// </summary>
        IImmutableList<IResolvedEffect> ResolvedEffects { get; }

        ImmutableHistoricData HistoricData { get; }

        Metadata Metadata { get; }

        IMutableState ToMutable();

        ImmutableState ToImmutable();
    }
}