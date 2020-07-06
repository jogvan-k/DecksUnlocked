using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public class ImmutableState : State
  {
    public Player PlayerTurn { get; }

    public int TurnNumber { get; }

    public bool IsGameOver { get; }

    public List<CoreAction> Actions { get; }

    public Dictionary<Player, Card[]> Decks { get; }

    public Dictionary<Player, Card[]> Hands { get; }

    public Dictionary<Player, Card[]> Discards { get; }

    public Dictionary<Player, Card[]> Archives { get; }

    public Dictionary<Player, List<Creature>> Fields { get; }

    public Queue<Effect> Effects { get; }

    public ImmutableState(Player playerTurn,
      int turnNumber,
      List<CoreAction> actions,
      Dictionary<Player, Card[]> decks,
      Dictionary<Player, Card[]> hands,
      Dictionary<Player, Card[]> discards,
      Dictionary<Player, Card[]> archives,
      Dictionary<Player, List<Creature>> fields,
      Queue<Effect> effects)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      Actions = actions;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Effects = effects;
    }
  }
}