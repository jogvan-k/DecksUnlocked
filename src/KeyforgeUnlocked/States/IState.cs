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
    House? ActiveHouse { get; }

    IImmutableDictionary<Player, int> Keys { get; }
    IImmutableDictionary<Player, int> Aember { get; }

    IImmutableList<IActionGroup> ActionGroups { get; }

    IImmutableDictionary<Player, IImmutableStack<Card>> Decks { get; }

    IImmutableDictionary<Player, IImmutableSet<Card>> Hands { get; }

    IImmutableDictionary<Player, IImmutableSet<Card>> Discards { get; }

    IImmutableDictionary<Player, IImmutableSet<Card>> Archives { get; }

    IImmutableDictionary<Player, IImmutableList<Creature>> Fields { get; }

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