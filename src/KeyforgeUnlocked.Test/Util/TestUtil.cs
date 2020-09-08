using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Types;
using UnlockedCore;

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

    public static Dictionary<Player, IMutableList<T>> Lists<T>(
      T player1Type)
    {
      return Lists(new[] {player1Type}, Enumerable.Empty<T>());
    }

    public static Dictionary<Player, IMutableList<T>> Lists<T>(
      T player1Type,
      T player2Type)
    {
      return Lists<T>(new[] {player1Type}, new[] {player2Type});
    }

    public static Dictionary<Player, IMutableList<T>> Lists<T>(
      IEnumerable<T> player1Types,
      IEnumerable<T> player2Types)
    {
      return new Dictionary<Player, IMutableList<T>>
      {
        {Player.Player1, new LazyList<T>(player1Types)},
        {Player.Player2, new LazyList<T>(player2Types)}
      };
    }

    public static IDictionary<Player, ISet<T>> Sets<T>(
      T player1Type)
    {
      return Sets<T>(new[] {player1Type}, Enumerable.Empty<T>());
    }

    public static IDictionary<Player, ISet<T>> Sets<T>(
      T player1Type,
      T player2Type)
    {
      return Sets<T>(new []{player1Type}, new []{player2Type});
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