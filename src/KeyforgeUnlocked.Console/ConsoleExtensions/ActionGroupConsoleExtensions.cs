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
        case EndTurnGroup a:
          return a.ToConsole();
        case PlayCreatureCardGroup a:
          return a.ToConsole();
        case PlayCardGroup a:
          return a.ToConsole();
        case UseCreatureGroup a:
          return a.ToConsole();
        case NoActionGroup a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this PlayCreatureCardGroup @group)
    {
      return $"Actions to card {@group.Card.Name}:";
    }

    static string ToConsole(this EndTurnGroup @group)
    {
      return "End turn";
    }

    static string ToConsole(this UseCreatureGroup group)
    {
      return $"Use creature";
    }

    static string ToConsole(this NoActionGroup group)
    {
      return "No action";
    }
  }
}