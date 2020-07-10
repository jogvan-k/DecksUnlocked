using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked;
using KeyforgeUnlocked.ActionGroup;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlockedConsole
{
  public static class KeyforgeStateConsoleExtensions
  {
    public static void Print(this IState state, out Dictionary<string, IActionGroup> commands)
    {
      var playerTurn = state.PlayerTurn;
      Console.WriteLine($"Current turn: {playerTurn}");
      Console.WriteLine($"Turn number: {state.TurnNumber}");
      commands = new Dictionary<string, IActionGroup>();
      PrintHand(state, commands);
      PrintAdditionalActions(state, commands);
    }

    static void PrintHand(IState state,
      Dictionary<string, IActionGroup> commands)
    {
      Console.WriteLine($"Cards in hand: ");
      int i = 1;
      foreach (var card in state.Hands[state.PlayerTurn])
      {
        var cardGroup = state.ActionGroups.SingleOrDefault(g => g.IsActionsRelatedToCard(card));
        if (cardGroup != default)
        {
          var command = $"p{i++}";
          commands.Add(command, cardGroup);
          Console.Write($"[{command}] ");
        }

        Console.WriteLine(card);
      }
    }

    static void PrintAdditionalActions(
      IState state,
      Dictionary<string, IActionGroup> commands)
    {
      var endAction = state.ActionGroups.SingleOrDefault(a => a.IsActionEndTurn());
      if (endAction != default)
      {
        commands.Add("end", endAction);
        Console.WriteLine("Additional actions: [end] End turn");
      }
    }

    static bool IsActionEndTurn(this IActionGroup group)
    {
      return group is EndTurnGroup;
    }

    static bool IsActionsRelatedToCard(
      this IActionGroup group,
      Card card)
    {
      if (group is PlayCard playCardGroup)
      {
        return playCardGroup.Card.Equals(card);
      }

      return false;
    }
  }
}