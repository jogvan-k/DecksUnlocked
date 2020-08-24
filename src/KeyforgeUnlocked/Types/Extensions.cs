using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public static class Extensions
  {
    public static ValidOn And(this ValidOn first, ValidOn second)
    {
      return (s, c) => first(s, c) && second(s, c);
    }

    public static ValidOn Or(this ValidOn first, ValidOn second)
    {
      return (s, c) => first(s, c) || second(s, c);
    }

    public static ValidOn Not(this ValidOn validOn)
    {
      return (s, c) => !validOn(s, c);
    }

    public static int Index(this IList<Creature> list, string id)
    {
      for (int i = 0; i < list.Count; i++)
      {
        if (list[i].Id == id)
          return i;
      }

      return -1;
    }

    public static Player Other(this Player player)
    {
      return player == Player.Player1 ? Player.Player2 : Player.Player1;
    }
  }
}