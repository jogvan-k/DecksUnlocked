using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public class ImmutableState : IState
  {
    public Player PlayerTurn { get; }

    public int TurnNumber { get; }

    public bool IsGameOver { get; }

    public List<CoreAction> Actions { get; }

    public Dictionary<Player, Stack<Card>> Decks { get; }

    public Dictionary<Player, ISet<Card>> Hands { get; }

    public Dictionary<Player, ISet<Card>> Discards { get; }

    public Dictionary<Player, ISet<Card>> Archives { get; }

    public Dictionary<Player, IList<Creature>> Fields { get; }

    public Queue<Effect> Effects { get; }

    public ImmutableState(Player playerTurn,
      int turnNumber,
      List<CoreAction> actions,
      Dictionary<Player, Stack<Card>> decks,
      Dictionary<Player, ISet<Card>> hands,
      Dictionary<Player, ISet<Card>> discards,
      Dictionary<Player, ISet<Card>> archives,
      Dictionary<Player, IList<Creature>> fields,
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

    public MutableState ToMutable()
    {
      // TODO clone fields
      return new MutableState(PlayerTurn, TurnNumber, Decks, Hands, Discards, Archives, Fields, Effects);
    }
  }
}