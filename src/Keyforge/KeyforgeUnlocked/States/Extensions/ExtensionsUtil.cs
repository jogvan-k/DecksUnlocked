using System.Collections.Generic;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States.Extensions
{
  static class ExtensionsUtil
  {
    public static bool TryRemove<T>(IReadOnlyDictionary<Player, IMutableSet<T>> toLookup,
      IIdentifiable id,
      out Player owningPlayer,
      out T? lookup) where T : IIdentifiable
    {
      foreach (var keyValue in toLookup)
      {
        if (TryRemove(keyValue.Value, id, out lookup))
        {
          owningPlayer = keyValue.Key;
          return true;
        }
      }

      owningPlayer = default;
      lookup = default;
      return false;
    }

    static bool TryRemove<T>(ICollection<T> toLookup,
      IIdentifiable id,
      out T? lookup) where T : IIdentifiable
    {
      foreach (var item in toLookup)
      {
        if (item.Equals(id))
        {
          lookup = item;
          toLookup.Remove(item);
          return true;
        }
      }

      lookup = default;
      return false;
    }
    
  }
}