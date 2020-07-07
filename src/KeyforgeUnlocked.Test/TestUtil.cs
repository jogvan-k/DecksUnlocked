using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest
{
  public static class TestUtil
  {
    public static MutableState EmptyMutableState => new MutableState(
      Player.Player1,
      0,
      new Dictionary<Player, Stack<Card>> {{Player.Player1, new Stack<Card>()}, {Player.Player2, new Stack<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>> {{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, IList<Creature>>
        {{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new Queue<Effect>());

    public static MutableState New(
      this State state,
      Player? playerTurn = null,
      int? turnNumber = null,
      Dictionary<Player, Stack<Card>> decks = null,
      Dictionary<Player, ISet<Card>> hands = null,
      Dictionary<Player, ISet<Card>> discards = null,
      Dictionary<Player, ISet<Card>> archives = null,
      Dictionary<Player, IList<Creature>> fields = null,
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
        effects ?? state.Effects);
    }

    public static Stack<Card> SampleDeck =>
      new Stack<Card>(Enumerable.Range(1, 36).Select(i => new SimpleCreatureCard()));
  }
}