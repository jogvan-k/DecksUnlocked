using System;
using KeyforgeUnlocked.ActionGroups;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class ActionGroupConsoleExtensions
  {
    public static string ToConsole(this IActionGroup actionGroup)
    {
      switch (actionGroup)
      {
        case PlayCard a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this PlayCard playCard)
    {
      return $"Actions to card {playCard.Card.Name}:";
    }
  }
}