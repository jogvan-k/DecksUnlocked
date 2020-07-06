using System.Collections.Generic;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Cards;
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
      new Dictionary<Player, Card[]>{{Player.Player1, new Card[0]}, {Player.Player2, new Card[0]}},
      new Dictionary<Player, Card[]>{{Player.Player1, new Card[0]}, {Player.Player2, new Card[0]}},
      new Dictionary<Player, Card[]>{{Player.Player1, new Card[0]}, {Player.Player2, new Card[0]}},
      new Dictionary<Player, Card[]>{{Player.Player1, new Card[0]}, {Player.Player2, new Card[0]}},
      new Dictionary<Player, List<Creature>>{{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new Queue<Effect>());
  }
}