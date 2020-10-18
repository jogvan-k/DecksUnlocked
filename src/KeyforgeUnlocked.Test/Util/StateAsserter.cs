using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Util
{
  public static class StateAsserter
  {
    public static void StateEquals(IState expected,
      IState actual)
    {
      if (expected.Equals(actual))
        return;
      var sb = new StringBuilder("States differ on the following parameters:\n");
      if (expected.PlayerTurn != actual.PlayerTurn)
        WriteFieldErrorMessage(
          sb, "Player turn", expected.PlayerTurn,
          actual.PlayerTurn);
      if (expected.TurnNumber != actual.TurnNumber)
        WriteFieldErrorMessage(
          sb, "Turn number", expected.TurnNumber,
          actual.TurnNumber);
      if (expected.IsGameOver != actual.IsGameOver)
        WriteFieldErrorMessage(
          sb, "IsGameOver", expected.IsGameOver,
          actual.IsGameOver);
      if (expected.ActiveHouse != actual.ActiveHouse)
        WriteFieldErrorMessage(
          sb, "ActiveHouse", expected.ActiveHouse,
          actual.ActiveHouse);
      CheckAndWritePreviousStateErrorMessage(sb, expected, actual);
      CheckAndWriteFieldErrorMessage(sb, "Keys", expected.Keys, actual.Keys);
      CheckAndWriteFieldErrorMessage(sb, "Aember", expected.Aember, actual.Aember);
      //if (!expected.ActionGroups.Equals(actual.ActionGroups))
      CheckAndWriteFieldErrorMessage(sb, "Decks", expected.Decks, actual.Decks);
      CheckAndWriteFieldErrorMessage(sb, "Hands", expected.Hands, actual.Hands);
      CheckAndWriteFieldErrorMessage(sb, "Discards", expected.Discards, actual.Discards);
      CheckAndWriteFieldErrorMessage(sb, "Archives", expected.Archives, actual.Archives);
      CheckAndWriteFieldErrorMessage(sb, "Fields", expected.Fields, actual.Fields);
      CheckAndWriteFieldErrorMessage(sb, "Effects", expected.Effects, actual.Effects);
      CheckAndWriteFieldErrorMessage(sb, "ResolvedEffects", expected.ResolvedEffects, actual.ResolvedEffects);
      //Metadata


      Assert.Fail(sb.ToString());
    }

    static void CheckAndWritePreviousStateErrorMessage(StringBuilder sb, IState expected, IState actual)
    {
      if (expected.PreviousState == null && actual.PreviousState != null)
      {
        AppendFieldName(sb, "PreviousState");
        sb.AppendLine("Expected has previousState set to null whereas actual don't.");
      }
      else if (actual.PreviousState == null && expected.PreviousState != null)
      {
        AppendFieldName(sb, "PreviousState");
        sb.AppendLine("Actual has previousState set to null whereas expected don't.");
      }
      else if (expected.PreviousState != null && expected.PreviousState.Equals(actual.PreviousState))
      {
        AppendFieldName(sb, "PreviousState");
        var expectedDepth = GetStateDepth(expected);
        var actualDepth = GetStateDepth(actual);
        if (actualDepth != expectedDepth)
          sb.AppendLine($"Expected's depth is {expectedDepth} but actual's depth is {actualDepth}");
        else
          sb.AppendLine(
            "Expected and actual have same depth previousState depth, but their previousState differ");
      }
    }

    static int GetStateDepth(IState state)
    {
      int depth = 0;
      var currentState = state;
      while (currentState.PreviousState != null)
      {
        currentState = currentState.PreviousState;
        depth++;
        if (depth > 10000)
          throw new Exception("Circular reference in state's previousState");
      }

      return depth;
    }

    static void WriteFieldErrorMessage<T>(
      StringBuilder sb,
      string fieldName,
      T expected,
      T actual)
    {
      sb.Append($"{fieldName}: Expected {expected}, actual {actual}\n");
    }

    static void CheckAndWriteFieldErrorMessage<T>(
      StringBuilder sb,
      string fieldName,
      IImmutableList<T> expected,
      IImmutableList<T> actual)
    {
      if (expected.SequenceEqual(actual)) return;
      sb.AppendLine($"{fieldName}:");
      var expectedExtra = expected.Except(actual);
      var actualExtra = actual.Except(expected);

      if (expectedExtra.Any())
        sb.AppendLine($"Expected has the following extra entries: {ToString(expectedExtra)}");
      if (actualExtra.Any())
        sb.AppendLine($"Actual has the following extra entries: {ToString(actualExtra)}");
    }

    static void CheckAndWriteFieldErrorMessage<T>(
      StringBuilder sb,
      string fieldName,
      IReadOnlyDictionary<Player, T> expected,
      IReadOnlyDictionary<Player, T> actual)
    {
      foreach (var key in expected.Keys.Union(actual.Keys))
      {
        if (!expected.ContainsKey(key))
        {
          AppendFieldName(sb, fieldName);
          sb.Append($"Expected state does not contain an entry for {key}");
        }

        else if (!actual.ContainsKey(key))
        {
          AppendFieldName(sb, fieldName);
          sb.Append($"Actual state does not contain an entry for {key}");
        }
        else
        {
          var ex = expected[key];
          var ac = actual[key];
          if (ex is IEnumerable<object> e && ac is IEnumerable<object> a)
            WriteEntryError(sb, fieldName, key, e, a);
          else if (ex is IMutableList<Creature> exC && ac is IMutableList<Creature> acC)
            WriteCreatureEntryError(sb, fieldName, key, exC, acC);
          else
            WriteEntryError(sb, fieldName, key, ex, ac);
        }
      }
    }

    static void WriteCreatureEntryError(StringBuilder sb,
      string fieldName,
      Player entry,
      IMutableList<Creature> expected,
      IMutableList<Creature> actual)
    {
      if (expected.SequenceEqual(actual))
        return;
      AppendFieldName(sb, fieldName);

      for (int i = 0; i < Math.Max(expected.Count, actual.Count); i++)
      {
        if (expected.Count < i + 1)
          sb.AppendLine($"{entry} expected don't have an entry at index {i} whereas actual has");
        else if (actual.Count < i + 1)
          sb.AppendLine($"{entry} actual don't have an entry at index {i} whereas expected has");
        else if (!expected[i].Equals(actual[i]))
          sb.AppendLine($"{entry} lists differ at index {i}. Expected: {expected[i]}, actual: {actual[i]}");
      }
    }

    static void WriteEntryError<T>(StringBuilder sb,
      string fieldName,
      Player entry,
      T expected,
      T actual)
    {
      if (!expected.Equals(actual))
      {
        AppendFieldName(sb, fieldName);
        sb.AppendLine($"For {entry} expected is {expected} but actual is {actual}");
      }
    }

    static void WriteEntryError<T>(StringBuilder sb,
      string fieldName,
      Player entry,
      IEnumerable<T> expected,
      IEnumerable<T> actual)
    {
      var expectedExtra = expected.Except(actual);
      var actualExtra = actual.Except(expected);

      if (expectedExtra.Any() || actualExtra.Any())
        AppendFieldName(sb, fieldName);

      if (expectedExtra.Any())
        sb.AppendLine($"Expected has the following extra values for {entry}: {ToString(expectedExtra)}");


      if (actualExtra.Any())
        sb.AppendLine($"Actual has the following extra values for {entry}: {ToString(actualExtra)}");
    }

    static string ToString<T>(IEnumerable<T> c)
    {
      return c.Select(s => s.ToString()).Aggregate((s1, s2) => string.Concat(s1.ToString(), ", ", s2.ToString()));
    }

    static void AppendFieldName(StringBuilder sb, string fieldName)
    {
      sb.AppendLine($"{fieldName}: ");
    }
  }
}