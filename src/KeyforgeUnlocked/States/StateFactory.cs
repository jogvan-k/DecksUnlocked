using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateFactory
  {
    public static ImmutableState Initiate(Deck player1Deck,
      Deck player2Deck)
    {
      var decks = new Dictionary<Player, Stack<Card>>
      {
        {Player.Player1, new Stack<Card>(player1Deck.Cards)},
        {Player.Player2, new Stack<Card>(player2Deck.Cards)}
      };

      var effects = new StackQueue<IEffect>(new[] {(IEffect) new InitiateGame()});
      return new MutableState(
          Player.Player1,
          1,
          false,
          null,
          EmptyValues(),
          EmptyValues(),
          new List<IActionGroup>(),
          decks,
          EmptySet(),
          EmptySet(),
          EmptySet(),
          EmptyField(),
          effects,
          new List<IResolvedEffect>())
        .ResolveEffects();
    }

    static Dictionary<Player, int> EmptyValues()
    {
      return new Dictionary<Player, int>
      {
        {Player.Player1, 0}, {Player.Player2, 0}
      };
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

    static Dictionary<Player, IList<Creature>> EmptyField()
    {
      return new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}
      };
    }
  }
}