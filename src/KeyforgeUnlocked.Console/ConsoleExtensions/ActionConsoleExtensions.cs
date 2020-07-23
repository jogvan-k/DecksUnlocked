using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;
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
        case DeclareHouse a:
          return a.ToConsole();
        case FightCreature a:
          return a.ToConsole();
        case UseCreatureAbility a:
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

    public static string ToConsole(this DeclareHouse action)
    {
      return $"Declare {action.House}";
    }

    public static string ToConsole(this FightCreature action)
    {
      return $"Attack {action.Creature.Card.Name}";
    }

    public static string ToConsole(this UseCreatureAbility action)
    {
      return "Use creature ability";
    }
  }
}