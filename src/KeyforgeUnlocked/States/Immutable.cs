using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public sealed class Immutable : StateBase
  {
    public Immutable(Player playerTurn,
      int turnNumber,
      Dictionary<Player, Stack<Card>> decks,
      Dictionary<Player, ISet<Card>> hands,
      Dictionary<Player, ISet<Card>> discards,
      Dictionary<Player, ISet<Card>> archives,
      Dictionary<Player, IList<Creature>> fields,
      Queue<IEffect> effects,
      List<IActionGroup> actionGroups)
      : base(
        playerTurn,
        turnNumber,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        actionGroups)
    {
    }
  }
}