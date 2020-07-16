using System;
using KeyforgeUnlocked.Actions;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class ActionConsoleExtensions
  {
    public static string ToConsole(this Action action)
    {
      switch (action)
      {
        case PlayCreature a:
          return a.ToConsole();
        case DiscardCard a:
          return a.ToConsole();
        case EndTurn a:
          return a.ToConsole();
        case Reap a:
          return a.ToConsole();
        case NoAction a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this PlayCreature playCard)
    {
      return $"Play to position {playCard.Position}";
    }


    static string ToConsole(this DiscardCard discardCard)
    {
      return $"Discard card";
    }

    public static string ToConsole(this EndTurn action)
    {
      return "End turn";
    }

    public static string ToConsole(this Reap action)
    {
      return "Reap creature";
    }

    public static string ToConsole(this NoAction action)
    {
      return "No action";
    }
  }
}