using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public interface IState : CoreState
  {
    public Dictionary<Player, Stack<Card>> Decks { get; }

    public Dictionary<Player, ISet<Card>> Hands { get; }

    public Dictionary<Player, ISet<Card>> Discards { get; }

    public Dictionary<Player, ISet<Card>> Archives { get; }

    public Dictionary<Player, IList<Creature>> Fields { get; }

    public Queue<Effect> Effects { get; }
    public MutableState ToMutable();
  }
}