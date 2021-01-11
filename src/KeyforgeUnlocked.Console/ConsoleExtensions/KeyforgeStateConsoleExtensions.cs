using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedConsole.PrintCommands;
using UnlockedCore;
using LogInfo = KeyforgeUnlockedConsole.ConsoleGames.LogInfo;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class KeyforgeStateConsoleExtensions
  {
    static IEvaluator _evaluator = new Evaluator();

    public static void Print(this IState state,
      ConsoleWriter consoleWriter,
      LogInfo logInfo,
      out Dictionary<string, IActionGroup> commands)
    {
      var fromPlayerPerspective = state.PlayerTurn;
      commands = new Dictionary<string, IActionGroup>();
      PrintStatus(state, fromPlayerPerspective, consoleWriter, logInfo, commands);
      PrintHand(state, fromPlayerPerspective, consoleWriter, commands);
      PrintResolvedEffects(state);
      PrintAdditionalActions(state, consoleWriter, commands);
      Console.WriteLine();
    }

    public static void PrintAITurn(
      this IState state,
      ConsoleWriter consoleWriter,
      LogInfo logInfo)
    {
      Console.Clear();
      var fromPlayerPerspective = state.PlayerTurn.Other();
      PrintStatus(state, fromPlayerPerspective, consoleWriter, logInfo);
      PrintHand(state, fromPlayerPerspective, consoleWriter);
      PrintResolvedEffects(state);
    }

    static void PrintStatus(IState state,
      Player fromPlayerPerspective,
      ConsoleWriter consoleWriter,
      LogInfo logInfo,
      Dictionary<string, IActionGroup>? commands = null)
    {
      Console.WriteLine($"Current player: {state.PlayerTurn}");
      if(logInfo == LogInfo.CalculationInfo)
        Console.WriteLine($"Board value: {_evaluator.Evaluate(state)}");
      PrintAmounts(state, fromPlayerPerspective);
      PrintActiveHouse(state);
      PrintFieldAndArtifacts(state, fromPlayerPerspective, consoleWriter, commands);
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

    static void PrintAmounts(IState state, Player fromPlayerPerspective)
    {
      Console.Write($"[Deck]: {state.Decks[fromPlayerPerspective].ToArray().Length} ");
      Console.Write($"[Dis]carde: {state.Discards[fromPlayerPerspective].Count} ");
      Console.WriteLine($"[Arc]hive: {state.Archives[fromPlayerPerspective].Count}");
    }

    static void PrintActiveHouse(IState state)
    {
      Console.WriteLine($"Active house: {state.ActiveHouse}");
    }

    static void PrintFieldAndArtifacts(IState state,
      Player fromPlayerPerspective,
      ConsoleWriter consoleWriter,
      Dictionary<string, IActionGroup>? commands = null)
    {
      Console.Write("Opponent: ");
      PrintKeysAndAember(state.Keys[fromPlayerPerspective.Other()], state.Aember[fromPlayerPerspective.Other()]);
      PrintField(state, state.Fields[fromPlayerPerspective.Other()], consoleWriter, commands);
      PrintArtifacts(state, state.Artifacts[fromPlayerPerspective.Other()], commands);

      Console.Write("You: ");
      PrintKeysAndAember(state.Keys[fromPlayerPerspective], state.Aember[fromPlayerPerspective]);
      PrintField(state, state.Fields[fromPlayerPerspective], consoleWriter, commands);
      PrintArtifacts(state, state.Artifacts[fromPlayerPerspective], commands);
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
      IImmutableList<Creature> creatures,
      ConsoleWriter consoleWriter,
      Dictionary<string, IActionGroup>? commands = null)
    {
      Console.WriteLine("Board: ");
      int i = 1;
      foreach (var creature in creatures)
      {
        if (commands != null)
        {
          var creatureGroup = state.ActionGroups.SingleOrDefault(c => c.IsActionsRelatedToCreature(creature));
          if (creatureGroup != default)
          {
            var command = $"c{i++}";
            commands.Add(command, creatureGroup);
            Console.Write($"[{command}]");
          }
        }

        consoleWriter.Write(creature.Card);
        var sb = new StringBuilder();
        sb.Append($", Power: {creature.Power}");
        if (creature.Armor > 0) sb.Append($", Armor: {creature.Armor}");
        if (creature.Damage > 0) sb.Append($", Damage: {creature.Damage}");
        if (creature.Aember > 0) sb.Append($", Aember: {creature.Aember}");
        if (creature.CardKeywords.Any()) sb.Append($", Keywords: ({KeywordsReadable(creature.CardKeywords)})");
        if (creature.IsWarded()) sb.Append(", Warded");
        if (creature.IsStunned()) sb.Append(", Stunned");
        if (creature.IsEnraged()) sb.Append(", Enraged");
        if (!creature.IsReady) sb.Append(", Exhausted");

        Console.WriteLine(sb.ToString());
      }

      Console.WriteLine();
    }

    static void PrintArtifacts(IState state,
      IImmutableSet<Artifact> artifacts,
      Dictionary<string, IActionGroup>? commands = null)
    {
      if (artifacts.Count == 0) return;
      Console.WriteLine("Artifacts: ");
      int i = 1;
      foreach (var artifact in artifacts)
      {
        var artifactGroup = state.ActionGroups.SingleOrDefault(c => c.IsActionRelatedToArtifact(artifact));
        if (artifactGroup != default && commands != null)
        {
          var command = $"a{i++}";
          commands.Add(command, artifactGroup);
          Console.Write($"[{command}]");
        }
        
        var sb = new StringBuilder($"{artifact.Card.Name}");
        if (!artifact.IsReady) sb.Append(", Exhausted");

        Console.WriteLine(sb.ToString());
      }
      
      Console.WriteLine();
    }

    static string KeywordsReadable(Keyword[] keywords)
    {
      if (keywords.Length == 0) return "";
      var sb = new StringBuilder();
      int i = 0;
      while (i < keywords.Length - 1)
        sb.Append($"{keywords[i++]}, ");
      sb.Append($"{keywords[i]}");
      return sb.ToString();
    }

    static void PrintHand(IState state,
      Player fromPlayerPerspective,
      ConsoleWriter consoleWriter,
      Dictionary<string, IActionGroup>? commands = null)
    {
      Console.WriteLine($"Cards in hand: ");
      int i = 1;
      foreach (var card in state.Hands[fromPlayerPerspective].OrderBy(c => c.House != state.ActiveHouse)
        .ThenBy(c => c.House).ThenBy(c => c.Name))
      {
        if (commands != null)
        {
          var cardGroup = state.ActionGroups.SingleOrDefault(g => g.IsActionsRelatedToCard(card));
          if (cardGroup != default)
          {
            var command = $"p{i++}";
            commands.Add(command, cardGroup);
            Console.Write($"[{command}] ");
          }
        }

        consoleWriter.WriteLine(card);
      }
    }

    static bool IsActionEndTurn(this IActionGroup group)
    {
      return group is EndTurnGroup || group is NoActionGroup;
    }

    static void PrintAdditionalActions(
      IState state,
      ConsoleWriter consoleWriter,
      Dictionary<string, IActionGroup> commands)
    {
      var specialActions = state.ActionGroups.SingleOrDefault(a => a.IsSpecialActionGroup());
      var endAction = state.ActionGroups.SingleOrDefault(a => a.IsActionEndTurn());
      var declareHouse = state.ActionGroups.SingleOrDefault(a => a.IsDeclareHouse());
      var takeArchive = state.ActionGroups.SingleOrDefault(a => a.IsTakeArchive());
      Console.WriteLine();
      if (specialActions != default)
      {
        commands.Add("action", specialActions);
        int i = 1;
        var origin = state.ToImmutable();
        foreach (var action in specialActions.Actions(origin))
        {
          Console.Write($"{i++}: ");
          action.WriteToConsole(consoleWriter);
        }
      }

      if (takeArchive != default)
      {
        commands.Add("take", takeArchive);
        Console.WriteLine("[Take] archive");
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
      return group.GetType().BaseType?.Name == typeof(ResolveEffectActionGroup<>).Name;
    }

    static bool IsTakeArchive(this IActionGroup group)
    {
      return group is TakeArchiveGroup;
    }

    static bool IsActionsRelatedToCard(
      this IActionGroup group,
      ICard card)
    {
      if (group is PlayActionCardGroup playActionCardGroup)
      {
        return playActionCardGroup.Card.Equals(card);
      }

      if (group is PlayCreatureCardGroup playCreatureCardGroup)
      {
        return playCreatureCardGroup.Card.Equals(card);
      }

      if (group is PlayArtifactCardGroup playArtifactCardGroup)
      {
        return playArtifactCardGroup.Card.Equals(card);
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

    static bool IsActionRelatedToArtifact(
      this IActionGroup group,
      Artifact artifact)
    {
      if (group is UseArtifactGroup actions)
      {
        return actions.Artifact.Equals(artifact);
      }
      
      return false;
    }

    public static ICard? GetCard(this IState state, string name)
    {
      return state.Metadata.InitialDecks.SelectMany(d => d.Value.ToHashSet())
        .FirstOrDefault(c => c.Name.ToLower().Contains(name));
    }
  }
}