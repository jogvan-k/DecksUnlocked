using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public static class StateFactory
  {
    public static IState Initiate(Deck player1Deck,
      Deck player2Deck)
    {
      var decks = new Dictionary<Player, Stack<Card>>
      {
        {Player.Player1, new Stack<Card>(player1Deck.Cards)},
        {Player.Player2, new Stack<Card>(player2Deck.Cards)}
      };

      return new MutableState(
          Player.Player1,
          1,
          false,
          null,
          new List<IActionGroup>(),
          decks,
          EmptySet(),
          EmptySet(),
          EmptySet(),
          EmptyField(),
          new Queue<IEffect>(),
          new List<IResolvedEffect>())
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

    static Dictionary<Player, IList<Creature>> EmptyField()
    {
      return new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}
      };
    }
  }
}