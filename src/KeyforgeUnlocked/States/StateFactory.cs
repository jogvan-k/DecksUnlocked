using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked
{
  public static class StateFactory
  {
    public static State Initiate(Deck player1Deck, Deck player2Deck)
    {
      var decks = new Dictionary<Player, Card[]>
        {{Player.Player1, player1Deck.Cards.ToArray()}, {Player.Player2, player2Deck.Cards.ToArray()}};

      return new MutableState(
        Player.Player1,
        1,
        decks,
        EmptyDictionary(),
        EmptyDictionary(),
        EmptyDictionary(),
        new Dictionary<Player, List<Creature>>(),
        new Queue<Effect>())
        .ResolveEffects();
    }

    public static State New(
      this State state,
      Player? playerTurn = null,
      int? turnNumber = null,
      Dictionary<Player, Card[]> decks = null,
      Dictionary<Player, Card[]> hands = null,
      Dictionary<Player, Card[]> discards = null,
      Dictionary<Player, Card[]> archives = null,
      Dictionary<Player, List<Creature>> fields = null,
      Queue<Effect> effects = null)
    {
      return new MutableState(
        playerTurn ?? state.PlayerTurn,
        turnNumber ?? state.TurnNumber,
        decks ?? state.Decks,
        hands ?? state.Hands,
        discards ?? state.Discards,
        archives ?? state.Archives,
        fields ?? state.Fields,
        effects ?? state.Effects)
        .ResolveEffects();
    }

    static Dictionary<Player, Card[]> EmptyDictionary()
    {
      return new Dictionary<Player, Card[]>{{Player.Player1, new Card[0]}, {Player.Player2, new Card[0]} };
    }
  }
}