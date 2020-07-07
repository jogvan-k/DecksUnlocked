using System.Collections;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using Microsoft.VisualBasic;
using UnlockedCore.States;

namespace KeyforgeUnlocked
{
  public static class StateFactory
  {
    public static State Initiate(Deck player1Deck,
      Deck player2Deck)
    {
      var decks = new Dictionary<Player, IList<Card>>
      {
        {Player.Player1, new List<Card>(player1Deck.Cards)},
        {Player.Player2, new List<Card>(player2Deck.Cards)}
      };

      return new MutableState(
          Player.Player1,
          1,
          decks,
          EmptySet(),
          EmptySet(),
          EmptySet(),
          new Dictionary<Player, IList<Creature>>(),
          new Queue<Effect>())
        .ResolveEffects();
    }

    static Dictionary<Player, IList<Card>> EmptyDeck<T>()
    {
      return new Dictionary<Player, IList<Card>>
      {
        {Player.Player1, new List<Card>()}, {Player.Player2, new List<Card>()}
      };
    }

    static Dictionary<Player, ISet<Card>> EmptySet()
    {
      return new Dictionary<Player, ISet<Card>>
      {
        {Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}
      };
    }
  }
}