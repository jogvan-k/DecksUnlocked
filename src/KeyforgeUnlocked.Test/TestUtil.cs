using System.Collections.Generic;
using KeyforgeUnlocked;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using Microsoft.VisualBasic;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest
{
  public static class TestUtil
  {
    public static MutableState EmptyMutableState => new MutableState(
      Player.Player1,
      0,
      new Dictionary<Player, IList<Card>>{{Player.Player1, new List<Card>()}, {Player.Player2, new List<Card>()}},
      new Dictionary<Player, ISet<Card>>{{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>>{{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, ISet<Card>>{{Player.Player1, new HashSet<Card>()}, {Player.Player2, new HashSet<Card>()}},
      new Dictionary<Player, IList<Creature>>{{Player.Player1, new List<Creature>()}, {Player.Player2, new List<Creature>()}},
      new Queue<Effect>());
  }
}