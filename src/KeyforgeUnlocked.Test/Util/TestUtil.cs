using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Util
{
  public static class TestUtil
  {
    public static Dictionary<Player, int> Ints(int player1Int, int player2Int)
    {
      return new Dictionary<Player, int>
      {
        {Player.Player1, player1Int},
        {Player.Player2, player2Int}
      };
    }

    public static Dictionary<Player, IList<T>> Lists<T>(
      T player1Type,
      T player2Type)
    {
      return Lists<T>(new[] {player1Type}, new[] {player2Type});
    }

    public static Dictionary<Player, IList<T>> Lists<T>(
      IEnumerable<T> player1Types,
      IEnumerable<T> player2Types)
    {
      return new Dictionary<Player, IList<T>>
      {
        {Player.Player1, new List<T>(player1Types)},
        {Player.Player2, new List<T>(player2Types)}
      };
    }

    public static IDictionary<Player, ISet<T>> Sets<T>(
      IEnumerable<T> player1Types,
      IEnumerable<T> player2Types)
    {
      return new Dictionary<Player, ISet<T>>
      {
        {Player.Player1, new HashSet<T>(player1Types)},
        {Player.Player2, new HashSet<T>(player2Types)}
      };
    }
  }
}