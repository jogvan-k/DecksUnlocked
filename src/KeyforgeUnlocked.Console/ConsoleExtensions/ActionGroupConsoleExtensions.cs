using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;

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
        case UseCreatureGroup a:
          return a.ToConsole();
        case NoActionGroup a:
          return a.ToConsole();
        case DeclareHouseGroup a:
          return a.ToConsole();
        case SingleTargetGroup a:
          return a.ToConsole();
        case PlayActionCardGroup a:
          return a.ToConsole();
        case TakeArchiveGroup a:
          return a.ToConsole();
        case PlayArtifactCardGroup a:
          return a.ToConsole();
        case UseArtifactGroup a:
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

    static string ToConsole(this DeclareHouseGroup group)
    {
      return $"Declare house";
    }

    static string ToConsole(this SingleTargetGroup group)
    {
      return $"Target creature";
    }

    static string ToConsole(this PlayActionCardGroup group)
    {
      return $"Play action";
    }
    
    static string ToConsole(this TakeArchiveGroup group)
    {
      return $"Take archive";
    }

    static string ToConsole(this PlayArtifactCardGroup group)
    {
      return $"Actions to card {@group.Card.Name}:";
    }

    static string ToConsole(this UseArtifactGroup group)
    {
      return $"Use artifact";
    }
  }
}