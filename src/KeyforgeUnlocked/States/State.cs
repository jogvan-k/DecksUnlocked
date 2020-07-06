using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked
{
  public interface State : CoreState
  {
    public Dictionary<Player, Card[]> Decks { get; }

    public Dictionary<Player, Card[]> Hands { get; }

    public Dictionary<Player, Card[]> Discards { get; }

    public Dictionary<Player, Card[]> Archives { get; }

    public Dictionary<Player, List<Creature>> Fields { get; }

    public Queue<Effect> Effects { get; }
  }
}