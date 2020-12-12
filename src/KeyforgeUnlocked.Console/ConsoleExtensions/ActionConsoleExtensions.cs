using System;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class ActionConsoleExtensions
  {
    public static string ToConsole(this IAction action)
    {
      switch (action)
      {
        case PlayCreatureCard a:
          return a.ToConsole();
        case PlayActionCard a:
          return a.ToConsole();
        case DiscardCard a:
          return a.ToConsole();
        case EndTurn a:
          return a.ToConsole();
        case Reap a:
          return a.ToConsole();
        case NoAction a:
          return a.ToConsole();
        case DeclareHouse a:
          return a.ToConsole();
        case FightCreature a:
          return a.ToConsole();
        case UseCreatureAbility a:
          return a.ToConsole();
        case RemoveStun a:
          return a.ToConsole();
        case TargetCreature a:
          return a.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this PlayCreatureCard playCard)
    {
      return $"Play to position {playCard.Position}";
    }

    static string ToConsole(this PlayActionCard playCard)
    {
      return $"Play action";
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

    public static string ToConsole(this DeclareHouse action)
    {
      return $"Declare {action.House}";
    }

    public static string ToConsole(this FightCreature action)
    {
      return $"Attack {action.Target.Card.Name}";
    }

    public static string ToConsole(this UseCreatureAbility action)
    {
      return "Use creature ability";
    }

    public static string ToConsole(this RemoveStun action)
    {
      return "Remove stun";
    }

    public static string ToConsole(this TargetCreature action)
    {
      return $"Target {action.Target.Card.Name}";
    }
  }
}