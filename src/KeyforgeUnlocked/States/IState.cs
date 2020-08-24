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
    House? ActiveHouse { get; }

    IImmutableDictionary<Player, int> Keys { get; }
    IImmutableDictionary<Player, int> Aember { get; }

    IImmutableList<IActionGroup> ActionGroups { get; }

    IImmutableDictionary<Player, Stack<Card>> Decks { get; }

    IImmutableDictionary<Player, ISet<Card>> Hands { get; }

    IImmutableDictionary<Player, ISet<Card>> Discards { get; }

    IImmutableDictionary<Player, ISet<Card>> Archives { get; }

    IImmutableDictionary<Player, IList<Creature>> Fields { get; }

    ImmutableArray<IEffect> Effects { get; }

    /// <summary>
    /// Effects that have been resolved since <see cref="Previousstate"/>
    /// </summary>
    IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    Metadata Metadata { get; }

    MutableState ToMutable();
  }
}