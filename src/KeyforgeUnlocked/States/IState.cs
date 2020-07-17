using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public interface IState : ICoreState
  {
    IState PreviousState { get; }
    House? ActiveHouse { get; }

    IDictionary<Player, int> Keys { get; }
    IDictionary<Player, int> Aember { get; }

    IList<IActionGroup> ActionGroups { get; }

    IDictionary<Player, Stack<Card>> Decks { get; }

    IDictionary<Player, ISet<Card>> Hands { get; }

    IDictionary<Player, ISet<Card>> Discards { get; }

    IDictionary<Player, ISet<Card>> Archives { get; }

    IDictionary<Player, IList<Creature>> Fields { get; }

    StackQueue<IEffect> Effects { get; }

    /// <summary>
    /// Effects that have been resolved since <see cref="PreviousState"/>
    /// </summary>
    IList<IResolvedEffect> ResolvedEffects { get; }

    Metadata Metadata { get; }

    MutableState ToMutable();
  }
}