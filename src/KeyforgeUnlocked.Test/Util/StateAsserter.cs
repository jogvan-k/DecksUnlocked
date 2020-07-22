using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using UnlockedCore.States;

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
      CheckAndWriteFieldErrorMessage(sb, "Keys", expected.Keys, actual.Keys);
      CheckAndWriteFieldErrorMessage(sb, "Aember", expected.Aember, actual.Aember);
      //if (!expected.ActionGroups.Equals(actual.ActionGroups))
      CheckAndWriteFieldErrorMessage(sb, "Decks", expected.Decks, actual.Decks);
      CheckAndWriteFieldErrorMessage(sb, "Hands", expected.Hands, actual.Hands);
      CheckAndWriteFieldErrorMessage(sb, "Discards", expected.Discards, actual.Discards);
      CheckAndWriteFieldErrorMessage(sb, "Archives", expected.Archives, actual.Archives);
      CheckAndWriteFieldErrorMessage(sb, "Fields", expected.Fields, actual.Fields);
      //CheckAndWriteFieldErrorMessage(sb, "Effects", expected.Effects, actual.Effects);
      //CheckAndWriteFieldErrorMessage(sb, "ResolvedEffects", expected.ResolvedEffects, actual.ResolvedEffects);
      //Metadata


      Assert.Fail(sb.ToString());
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
      IImmutableDictionary<Player, T> expected,
      IImmutableDictionary<Player, T> actual)
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
          else if(ex is IEnumerable<Creature> exC && ac is IEnumerable<Creature> acC)
            WriteEntryError(sb, fieldName, key, exC.Cast<object>(), acC.Cast<object>());
          else
            WriteEntryError(sb, fieldName, key, ex, ac);
        }
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
      if (expectedExtra.Any())
      {
        AppendFieldName(sb, fieldName);
        sb.AppendLine($"Expected has the following extra values for {entry}: {ToString(expectedExtra)}");
      }

      if (actualExtra.Any())
      {
        AppendFieldName(sb, fieldName);
        sb.AppendLine($"Actual has the following extra values for {entry}: {ToString(actualExtra)}");
      }
    }

    static string ToString<T>(IEnumerable<T> c)
    {
      return c.Select(s => s.ToString()).Aggregate((s1, s2) => string.Concat(s1.ToString(), ", ", s2.ToString()));
    }

    static void AppendFieldName(StringBuilder sb, string fieldName)
    {
      sb.Append($"{fieldName}: ");
    }
  }
}