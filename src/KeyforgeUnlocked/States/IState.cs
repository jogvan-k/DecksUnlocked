using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public interface IState : ICoreState
  {
    IState PreviousState { get; }

    IList<IResolvedEffect> ResolvedEffects { get; }

    IList<IActionGroup> ActionGroups { get; }

    IDictionary<Player, Stack<Card>> Decks { get; }

    IDictionary<Player, ISet<Card>> Hands { get; }

    IDictionary<Player, ISet<Card>> Discards { get; }

    IDictionary<Player, ISet<Card>> Archives { get; }

    IDictionary<Player, IList<Creature>> Fields { get; }

    Queue<IEffect> Effects { get; }
    MutableState ToMutable();
  }
}