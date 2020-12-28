using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public interface IState : ICoreState
  {
    bool IsGameOver { get; }
    House? ActiveHouse { get; }
    IReadOnlyDictionary<Player, int> Keys { get; }
    IReadOnlyDictionary<Player, int> Aember { get; }
    IImmutableSet<IActionGroup> ActionGroups { get; }
    IReadOnlyDictionary<Player, IImmutableStack<ICard>> Decks { get; }
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> Hands { get; }
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> Discards { get; }
    IReadOnlyDictionary<Player, IImmutableSet<ICard>> Archives { get; }
    IReadOnlyDictionary<Player, IImmutableList<Creature>> Fields { get; }
    IReadOnlyDictionary<Player, IImmutableSet<Artifact>> Artifacts { get; }
    ImmutableArray<IEffect> Effects { get; }

    /// <summary>
    /// Effects that have been resolved since <see cref="Previousstate"/>
    /// </summary>
    IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    IImmutableHistoricData HistoricData { get; }
      
    Metadata Metadata { get; }

    MutableState ToMutable();

    ImmutableState ToImmutable();
  }
}