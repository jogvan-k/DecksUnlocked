using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
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
      PrintActiveHouse(state);
      PrintField(state, commands);
    }

    static void PrintResolvedEffects(IState state)
    {
      if (state.ResolvedEffects.Any())
        Console.WriteLine();
      foreach (var effect in state.ResolvedEffects)
      {
        Console.WriteLine(effect.ToString());
      }
    }

    static void PrintAmounts(IState state)
    {
      var playerTurn = state.PlayerTurn;
      Console.Write($"[Deck]: {state.Decks[playerTurn].Count} ");
      Console.Write($"[Dis]carde: {state.Discards[playerTurn].Count} ");
      Console.WriteLine($"[Arc]hive: {state.Archives[playerTurn].Count}");
    }

    static void PrintActiveHouse(IState state)
    {
      Console.WriteLine($"Active house: {state.ActiveHouse}");
    }

    static void PrintField(IState state,
      Dictionary<string, IActionGroup> commands)
    {
      Console.Write("Opponent: ");
      var playerTurn = state.PlayerTurn;
      PrintKeysAndAember(state.Keys[playerTurn.Other()], state.Aember[playerTurn.Other()]);
      PrintField(state, state.Fields[playerTurn.Other()], commands);

      Console.Write("You: ");
      PrintKeysAndAember(state.Keys[playerTurn], state.Aember[playerTurn]);
      PrintField(state, state.Fields[playerTurn], commands);
    }

    static void PrintKeysAndAember(int keys,
      int aember)
    {
      var k = keys == 0
        ? ""
        : Enumerable.Repeat("*", keys).Aggregate(
          (a,
            b) => a + b);
      Console.WriteLine($"Æmber: {k}{aember}");
    }

    static void PrintField(IState state,
      IList<Creature> creatures,
      Dictionary<string, IActionGroup> commands)
    {
      Console.WriteLine("Board: ");
      int i = 1;
      foreach (var creature in creatures)
      {
        var creatureGroup = state.ActionGroups.SingleOrDefault(c => c.IsActionsRelatedToCreature(creature));
        if (creatureGroup != default)
        {
          var command = $"c{i++}";
          commands.Add(command, creatureGroup);
          Console.Write($"[{command}]");
        }
        var sb = new StringBuilder($"{creature.Card.Name}");
        sb.Append($", Power: {creature.Power}");
        if (creature.Armor > 0) sb.Append($", Armor: {creature.Armor}");
        if (creature.Damage > 0) sb.Append($", Damage: {creature.Damage}");
        if (creature.Aember > 0) sb.Append($", Aember: {creature.Aember}");
        if (creature.Keywords.Any()) sb.Append($", Keywords: ({KeywordsReadable(creature.Keywords)})");
        if (creature.IsWarded()) sb.Append(", Warded");
        if (creature.IsStunned()) sb.Append(", Stunned");
        if (creature.IsEnraged()) sb.Append(", Enraged");
        if (!creature.IsReady) sb.Append(", Exhausted");

        Console.WriteLine(sb.ToString());
      }

      Console.WriteLine();
    }

    static string KeywordsReadable(Keyword[] keywords)
    {
      if (keywords.Length == 0) return "";
      var sb = new StringBuilder();
      int i = 0;
      while(i < keywords.Length - 1)
        sb.Append($"{keywords[i++]}, ");
      sb.Append($"{keywords[i]}");
      return sb.ToString();
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

    static bool IsActionEndTurn(this IActionGroup group)
    {
      return group is EndTurnGroup;
    }

    static void PrintAdditionalActions(
      IState state,
      Dictionary<string, IActionGroup> commands)
    {
      var specialActions = state.ActionGroups.SingleOrDefault(a => a.IsSpecialActionGroup());
      var endAction = state.ActionGroups.SingleOrDefault(a => a.IsActionEndTurn());
      var declareHouse = state.ActionGroups.SingleOrDefault(a => a.IsDeclareHouse());
      Console.WriteLine();
      if (specialActions != default)
      {
        commands.Add("action", specialActions);
        int i = 1;
        foreach (var action in specialActions.Actions)
        {
          Console.WriteLine($"{i++}: {action.ToConsole()}");
        }
      }
      if (endAction != default)
      {
        commands.Add("end", endAction);
        Console.Write("[End] turn ");
      }

      if (declareHouse != default)
      {
        commands.Add("house", declareHouse);
        Console.Write("Declare [House] ");
      }
      Console.WriteLine("");
    }

    static bool IsDeclareHouse(this IActionGroup group)
    {
      return group is DeclareHouseGroup;
    }

    static bool IsSpecialActionGroup(this IActionGroup group)
    {
      return group is ResolveEffectActionGroup;
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
      Creature creature)
    {
      if (group is UseCreatureGroup actions)
      {
        return actions.Creature.Equals(creature);
      }

      return false;
    }
  }
}