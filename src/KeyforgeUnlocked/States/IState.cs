using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroup;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public interface IState : CoreState
  {
    public List<IActionGroup> ActionGroups { get; }

    Dictionary<Player, Stack<Card>> Decks { get; }

    Dictionary<Player, ISet<Card>> Hands { get; }

    Dictionary<Player, ISet<Card>> Discards { get; }

    Dictionary<Player, ISet<Card>> Archives { get; }

    Dictionary<Player, IList<Creature>> Fields { get; }

    Queue<Effect> Effects { get; }
    MutableState ToMutable();
  }
}