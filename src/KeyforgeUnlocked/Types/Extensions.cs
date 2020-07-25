using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.Types
{
  public static class Extensions
  {
    public static int Index(this IList<Creature> list, string id)
    {
      for (int i = 0; i < list.Count; i++)
      {
        if (list[i].Id == id)
          return i;
      }

      return -1;
    }
  }
}