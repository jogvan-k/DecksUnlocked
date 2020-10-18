using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public interface IState : ICoreState
  {
    IState PreviousState { get; }
    bool IsGameOver { get; }
    House? ActiveHouse { get; }
    IReadOnlyDictionary<Player, int> Keys { get; }
    IReadOnlyDictionary<Player, int> Aember { get; }
    IImmutableList<IActionGroup> ActionGroups { get; }
    IReadOnlyDictionary<Player, IImmutableStack<Card>> Decks { get; }
    IReadOnlyDictionary<Player, IImmutableSet<Card>> Hands { get; }
    IReadOnlyDictionary<Player, IImmutableSet<Card>> Discards { get; }
    IReadOnlyDictionary<Player, IImmutableSet<Card>> Archives { get; }
    IReadOnlyDictionary<Player, IImmutableList<Creature>> Fields { get; }
    ImmutableArray<IEffect> Effects { get; }

    /// <summary>
    /// Effects that have been resolved since <see cref="Previousstate"/>
    /// </summary>
    IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    Metadata Metadata { get; }

    MutableState ToMutable();

    ImmutableState ToImmutable();
  }
}