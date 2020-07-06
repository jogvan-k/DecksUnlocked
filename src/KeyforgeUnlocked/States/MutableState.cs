using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  // TODO Refactor to popsicle immutable?
  public class MutableState : State
  {
    public bool IsGameOver { get; set; }
    public List<CoreAction> Actions { get; private set; }

    public Player PlayerTurn { get; set; }

    public int TurnNumber { get; set; }

    public Dictionary<Player, Card[]> Decks { get; set; }

    public Dictionary<Player, Card[]> Hands { get; set; }

    public Dictionary<Player, Card[]> Discards { get; set; }

    public Dictionary<Player, Card[]> Archives { get; set; }

    public Dictionary<Player, List<Creature>> Fields { get; set; }

    public Queue<Effect> Effects { get; set; }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      Dictionary<Player, Card[]> decks,
      Dictionary<Player, Card[]> hands,
      Dictionary<Player, Card[]> discards,
      Dictionary<Player, Card[]> archives,
      Dictionary<Player, List<Creature>> fields,
      Queue<Effect> effects)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Effects = effects;
      Actions = new List<CoreAction>();
    }

    public void Draw(Player player,
      int cards)
    {
      if (cards <= 0)
      {
        return;
      }

      if (!Decks.TryReduce(
        player,
        cards,
        out var newDecks,
        out var extraCards))
      {
        // shuffle and draw remaining
      }

      Decks = newDecks;
      Hands = new Dictionary<Player, Card[]>
        {{player, Hands[player].Concat(extraCards)}, {player.Other(), Hands[player.Other()]}};
    }

    ImmutableState Immutable()
    {
      return new ImmutableState(
        PlayerTurn,
        TurnNumber,
        Actions,
        Decks,
        Hands,
        Discards,
        Archives,
        Fields,
        Effects);
    }

    void RefreshBaseActions()
    {
      Actions = IsGameOver ? new List<CoreAction>() : new List<CoreAction>() {new EndTurn(this)};
    }

    public ImmutableState ResolveEffects()
    {
      while (Actions.Count == 0 && Effects.Count != 0)
        Effects.Dequeue().Resolve(this);

      if (Actions.Count == 0)
        RefreshBaseActions();

      return this.Immutable();
    }
  }
}