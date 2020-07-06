using System;
using KeyforgeUnlocked.Actions;
using UnlockedCore.Actions;

namespace KeyforgeUnlockedConsole
{
  public static class ActionConsoleExtensions
  {
    public static string ToConsole(this CoreAction coreAction)
    {
      switch (coreAction)
      {
        case EndTurn a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    public static string ToConsole(this EndTurn action)
    {
      return "End turn";
    }
  }
}