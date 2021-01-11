using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlockedConsole.PrintCommands;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class ActionConsoleExtensions
  {
    public static void WriteToConsole(this IAction action, ConsoleWriter cw)
    {
      switch (action)
      {
        case DeclareHouse a:
          Console.Write("Declare ");
          cw.WriteLine(a.House);
          return;
        case FightCreature a:
          Console.Write("Attack ");
          cw.WriteLine(a.Target.Card);
          return;
        case TargetAction a:
          Console.Write("Target ");
          cw.WriteLine(a.Target);
          return;
        default:
          Console.WriteLine(action.ToString());
          return;
      }
    }
  }
}