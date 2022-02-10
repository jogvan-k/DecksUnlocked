using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
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
            CheckAndWriteFieldErrorMessage(sb, "Keys", expected.Keys, actual.Keys);
            CheckAndWriteFieldErrorMessage(sb, "Aember", expected.Aember, actual.Aember);
            //if (!expected.ActionGroups.Equals(actual.ActionGroups))
            CheckAndWriteFieldErrorMessage(sb, "Decks", expected.Decks, actual.Decks);
            CheckAndWriteFieldErrorMessage(sb, "Hands", expected.Hands, actual.Hands);
            CheckAndWriteFieldErrorMessage(sb, "Discards", expected.Discards, actual.Discards);
            CheckAndWriteFieldErrorMessage(sb, "Archives", expected.Archives, actual.Archives);
            CheckAndWriteFieldErrorMessage(sb, "Fields", expected.Fields, actual.Fields);
            CheckAndWriteFieldErrorMessage(sb, "Artifacts", expected.Artifacts, actual.Artifacts);
            CheckAndWriteFieldErrorMessage(sb, "Effects", expected.Effects, actual.Effects);
            CheckAndWriteFieldErrorMessage(sb, "ResolvedEffects", expected.ResolvedEffects, actual.ResolvedEffects);
            CheckAndWriteFieldErrorMessage(sb, "Events", expected.Events, actual.Events);
            if (!expected.HistoricData.Equals(actual.HistoricData))
                sb.AppendLine("Historic data");
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

        // static void CheckAndWriteFieldErrorMessage<T>(
        //   StringBuilder sb,
        //   string fieldName,
        //   IReadOnlyDictionary<Player, IImmutableStack<T>> expected,
        //   IReadOnlyDictionary<Player, IImmutableStack<T>> actual)
        // {
        //   CheckAndWriteFieldErrorMessage(
        //     sb,
        //     fieldName,
        //     expected.ToImmutableDictionary(kv => kv.Key, kv => kv.Value.ToList()),
        //     actual.ToImmutableDictionary(kv => kv.Key, kv => kv.Value.ToList()));
        // }
        static void CheckAndWriteFieldErrorMessage<T>(
            StringBuilder sb,
            string fieldName,
            IReadOnlyDictionary<Player, IImmutableStack<T>> expected,
            IReadOnlyDictionary<Player, IImmutableStack<T>> actual)
        {
            foreach (var key in expected.Keys)
            {
                if (!AssertKey(sb, fieldName, key, expected, actual)) continue;
                if (expected[key].Count() != actual[key].Count())
                {
                    AppendFieldName(sb, fieldName);
                    sb.Append($"Expected and actual have different length for key {key}");
                    return;
                }

                for (int i = 0; i < expected[key].Count(); i++)
                {
                    if (!expected[key].ToArray()[i].Equals(actual[key].ToArray()[i]))
                    {
                        AppendFieldName(sb, fieldName);
                        sb.Append($"Expected and actual differ at index {i} for key {key}");
                        return;
                    }
                }
            }
        }

        static void CheckAndWriteFieldErrorMessage<T>(
            StringBuilder sb,
            string fieldName,
            IReadOnlyDictionary<Player, T> expected,
            IReadOnlyDictionary<Player, T> actual)
        {
            foreach (var key in expected.Keys.Union(actual.Keys))
            {
                if (!AssertKey(sb, fieldName, key, expected, actual)) continue;
                var ex = expected[key];
                var ac = actual[key];
                if (ex is IEnumerable<object> e && ac is IEnumerable<object> a)
                    WriteEntryError(sb, fieldName, key, e, a);
                else if (ex is IMutableList<Creature> exCm && ac is IMutableList<Creature> acCm)
                    WriteCreatureEntryError(sb, fieldName, key, exCm, acCm);
                else if (ex is IImmutableList<Creature> exCim && ac is IImmutableList<Creature> acCim)
                    WriteCreatureEntryError(sb, fieldName, key, exCim, acCim);
                else if (ex is IMutableSet<Artifact> exAm && ac is IMutableSet<Artifact> acAm)
                    WriteArtifactEntryError(sb, fieldName, key, exAm, acAm);
                else if (ex is IImmutableSet<Artifact> exAim && ac is IImmutableSet<Artifact> acAim)
                    WriteArtifactEntryError(sb, fieldName, key, exAim, acAim);
                else
                    WriteEntryError(sb, fieldName, key, ex, ac);
            }
        }

        static bool AssertKey<TKey, TValue>(
            StringBuilder sb,
            string fieldName,
            TKey key,
            IReadOnlyDictionary<TKey, TValue> expected,
            IReadOnlyDictionary<TKey, TValue> actual)
        {
            if (!expected.ContainsKey(key))
            {
                AppendFieldName(sb, fieldName);
                sb.Append($"Expected state does not contain an entry for {key}");
                return false;
            }

            if (!actual.ContainsKey(key))
            {
                AppendFieldName(sb, fieldName);
                sb.Append($"Actual state does not contain an entry for {key}");
                return false;
            }

            return true;
        }

        static void CheckAndWriteFieldErrorMessage(
            StringBuilder sb,
            string fieldName,
            ImmutableEvents expected,
            ImmutableEvents actual)
        {
            if (expected.Equals(actual)) return;
            sb.AppendLine(fieldName);
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

        static void WriteCreatureEntryError(StringBuilder sb,
            string fieldName,
            Player entry,
            IImmutableList<Creature> expected,
            IImmutableList<Creature> actual)
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

        static void WriteArtifactEntryError(
            StringBuilder sb,
            string fieldName,
            Player entry,
            IMutableSet<Artifact> expected,
            IMutableSet<Artifact> actual)
        {
            if (expected.SetEquals(actual))
                return;
            AppendFieldName(sb, fieldName);

            foreach (var difference in expected.Except(actual))
            {
                sb.AppendLine($"{entry} expected has the entry ({difference}) whereas actual don't");
            }

            foreach (var difference in actual.Except(expected))
            {
                sb.AppendLine($"{entry} actual has the entry ({difference}) whereas expected don't");
            }
        }

        static void WriteArtifactEntryError(
            StringBuilder sb,
            string fieldName,
            Player entry,
            IImmutableSet<Artifact> expected,
            IImmutableSet<Artifact> actual)
        {
            if (expected.SetEquals(actual))
                return;
            AppendFieldName(sb, fieldName);

            foreach (var difference in expected.Except(actual))
            {
                sb.AppendLine($"{entry} expected has the entry ({difference}) whereas actual don't");
            }

            foreach (var difference in actual.Except(expected))
            {
                sb.AppendLine($"{entry} actual has the entry ({difference}) whereas expected don't");
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