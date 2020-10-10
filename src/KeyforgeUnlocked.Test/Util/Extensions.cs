using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlockedTest.Util
{
  public static class Extensions
  {
    public static TimeSpan Total(this IEnumerable<TimeSpan> timeSpans)
    {
      var total = new TimeSpan();
      foreach (var timeSpan in timeSpans)
      {
        total = total.Add(timeSpan);
      }

      return total;
    }

    public static TimeSpan Average(this IEnumerable<TimeSpan> timeSpans)
    {
      var total = timeSpans.Total();
      if (!timeSpans.Any())
        return total;
      return total.Divide(timeSpans.Count());
    }
  }
}