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
        case EndTurnGroup a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this PlayCreatureCardGroup playCard)
    {
      return $"Actions to card {playCard.Card.Name}:";
    }

    static string ToConsole(this EndTurnGroup endTurn)
    {
      return "End turn";
    }
  }
}