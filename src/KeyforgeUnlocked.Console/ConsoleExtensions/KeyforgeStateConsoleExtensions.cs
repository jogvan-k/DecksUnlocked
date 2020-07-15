using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class KeyforgeStateConsoleExtensions
  {
    public static void Print(this IState state,
      out Dictionary<string, IActionGroup> commands)
    {
      commands = new Dictionary<string, IActionGroup>();
      PrintStatus(state, commands);
      PrintHand(state, commands);
      PrintResolvedEffects(state);
      PrintAdditionalActions(state, commands);
    }

    static void PrintStatus(IState state,
      Dictionary<string, IActionGroup> commands)
    {
      Console.WriteLine($"Current player: {state.PlayerTurn}");
      PrintAmounts(state);
      PrintField(state, commands);
    }

    static void PrintResolvedEffects(IState state)
    {
      if(state.ResolvedEffects.Any())
        Console.WriteLine();
      foreach (var effect in state.ResolvedEffects)
      {
        Console.WriteLine(effect.ToConsole());
      }
    }

    static void PrintField(IState state,
      Dictionary<string, IActionGroup> commands)
    {
      Console.Write("Opponent's board: ");
      PrintField(state, state.Fields[state.PlayerTurn.Other()], commands);
      Console.Write("Your board:       ");
      PrintField(state, state.Fields[state.PlayerTurn], commands);
    }

    static void PrintField(IState state,
      IList<Creature> creatures,
      Dictionary<string, IActionGroup> commands)
    {
      int i = 0;
      foreach (var creature in creatures)
      {
        var creatureGroup = state.ActionGroups.SingleOrDefault(c => c.IsActionsRelatedToCreature(creature));
        if (creatureGroup != default)
        {
          var command = $"c{i++}";
          commands.Add(command, creatureGroup);
          Console.Write($"[{command}]");
        }
        Console.Write($"{creature.Card.Name} ");
      }

      Console.WriteLine();
    }

    static void PrintHand(IState state,
      Dictionary<string, IActionGroup> commands)
    {
      Console.WriteLine();
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
        Console.WriteLine();
        commands.Add("end", endAction);
        Console.WriteLine("Additional actions: [End] turn");
      }
    }

    static void PrintAmounts(IState state)
    {
      var playerTurn = state.PlayerTurn;
      Console.Write($"[Deck]: {state.Decks[playerTurn].Count} ");
      Console.Write($"[Dis]carde: {state.Discards[playerTurn].Count} ");
      Console.WriteLine($"[Arc]hive: {state.Archives[playerTurn].Count}");
    }

    static bool IsActionEndTurn(this IActionGroup group)
    {
      return group is EndTurnGroup;
    }

    static bool IsActionsRelatedToCard(
      this IActionGroup group,
      Card card)
    {
      if (group is PlayCardGroup playCardGroup)
      {
        return playCardGroup.Card.Equals(card);
      }

      return false;
    }

    static bool IsActionsRelatedToCreature(
      this IActionGroup group,
      ICreature creature)
    {
      if (group is UseCreatureGroup actions)
      {
        return actions.Creature.Equals(creature);
      }

      return false;
    }
  }
}