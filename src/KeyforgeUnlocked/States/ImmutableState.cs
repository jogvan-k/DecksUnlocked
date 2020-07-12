using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public sealed class ImmutableState : StateBase, IState
  {
    public Player PlayerTurn => playerTurn;

    public int TurnNumber => turnNumber;

    public bool IsGameOver => isGameOver;

    public IList<IActionGroup> ActionGroups => actionGroups;

    public IDictionary<Player, Stack<Card>> Decks => decks;

    public IDictionary<Player, ISet<Card>> Hands => hands;

    public IDictionary<Player, ISet<Card>> Discards => discards;

    public IDictionary<Player, ISet<Card>> Archives => archives;

    public IDictionary<Player, IList<Creature>> Fields => fields;

    public Queue<IEffect> Effects => effects;
  
    public ImmutableState(Player playerTurn,
      int turnNumber,
      IDictionary<Player, Stack<Card>> decks,
      IDictionary<Player, ISet<Card>> hands,
      IDictionary<Player, ISet<Card>> discards,
      IDictionary<Player, ISet<Card>> archives,
      IDictionary<Player, IList<Creature>> fields,
      Queue<IEffect> effects,
      IList<IActionGroup> actionGroups)
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