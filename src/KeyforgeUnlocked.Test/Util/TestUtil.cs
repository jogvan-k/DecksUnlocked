using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Util
{
  public static class TestUtil
  {
    public static KeyforgeUnlocked.Types.Lookup<Player, int> Ints()
    {
      return new Dictionary<Player, int>
      {
        {Player.Player1, 0},
        {Player.Player2, 0}
      }.ToLookup();
    }
    public static KeyforgeUnlocked.Types.Lookup<Player, int> Ints(int player1Int, int player2Int)
    {
      return new Dictionary<Player, int>
      {
        {Player.Player1, player1Int},
        {Player.Player2, player2Int}
      }.ToLookup();
    }

    public static LookupReadOnly<Player, IMutableList<T>> Lists<T>()
    {
      return Lists(Enumerable.Empty<T>(), Enumerable.Empty<T>());
    }
    
    public static LookupReadOnly<Player, IMutableList<T>> Lists<T>(
      T player1Type)
    {
      return Lists(new[] {player1Type}, Enumerable.Empty<T>());
    }

    public static LookupReadOnly<Player, IMutableList<T>> Lists<T>(
      T player1Type,
      T player2Type)
    {
      return Lists<T>(new[] {player1Type}, new[] {player2Type});
    }

    public static LookupReadOnly<Player, IMutableList<T>> Lists<T>(
      IEnumerable<T> player1Types,
      IEnumerable<T> player2Types)
    {
      return new Dictionary<Player, IMutableList<T>>
      {
        {Player.Player1, new LazyList<T>(player1Types)},
        {Player.Player2, new LazyList<T>(player2Types)}
      }.ToReadOnly();
    }
    public static LookupReadOnly<Player, IMutableStackQueue<T>> Stacks<T>()
    {
      return Stacks(Enumerable.Empty<T>(), Enumerable.Empty<T>());
    }

    public static LookupReadOnly<Player, IMutableStackQueue<T>> Stacks<T>(
      IEnumerable<T> player1Type)
    {
      return Stacks(player1Type, Enumerable.Empty<T>());
    }

    public static LookupReadOnly<Player, IMutableStackQueue<T>> Stacks<T>(
      IEnumerable<T> player1Type,
      IEnumerable<T> player2Type)
    {
      return new Dictionary<Player, IMutableStackQueue<T>>
      {
        {Player.Player1, new LazyStackQueue<T>(player1Type)},
        {Player.Player2, new LazyStackQueue<T>(player2Type)}
      }.ToReadOnly();
    }

    public static LookupReadOnly<Player, IMutableSet<T>> Sets<T>()
    {
      return Sets(Enumerable.Empty<T>(), Enumerable.Empty<T>());
    }

    public static LookupReadOnly<Player, IMutableSet<T>> Sets<T>(
      T player1Type)
    {
      return Sets(new[] {player1Type}, Enumerable.Empty<T>());
    }

    public static LookupReadOnly<Player, IMutableSet<T>> Sets<T>(
      T player1Type,
      T player2Type)
    {
      return Sets<T>(new[] {player1Type}, new[] {player2Type});
    }

    public static LookupReadOnly<Player, IMutableSet<T>> Sets<T>(
      IEnumerable<T> player1Types,
      IEnumerable<T> player2Types)
    {
      return new Dictionary<Player, IMutableSet<T>>
      {
        {Player.Player1, new LazySet<T>(player1Types)},
        {Player.Player2, new LazySet<T>(player2Types)}
      }.ToReadOnly();
    }
  }
}