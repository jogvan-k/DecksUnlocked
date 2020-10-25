using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;  
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.AITypes;

namespace KeyforgeUnlockedTest.Benchmark
{
  [TestFixture]
  sealed class BenchmarkTest
  {
    IState _state;

    [Test]
    [Explicit]
    public void StateRun()
    {
      var i = 0;
      _state = SetupStartState();

      var stopwatch = Stopwatch.StartNew();
      while (!_state.IsGameOver && ++i > -1)
      {
        var coreActions = _state.Actions();
        if (coreActions.Length > 1)
          _state = (IState) coreActions[1].DoCoreAction();
        else
          _state = (IState) coreActions.First().DoCoreAction();
      }

      stopwatch.Stop();
      Console.WriteLine($"Ran through {i} states in {stopwatch.Elapsed.TotalSeconds} seconds.");
    }

    [Test]
    [Explicit]
    public void MinimaxAIRun()
    {
      _state = SetupStartState();
      var ai = new MinimaxAI(new Evaluator(), 2, SearchDepthConfiguration.turn, AIMethods.SearchConfiguration.NoRestrictions, AIMethods.LoggingConfiguration.LogAll);

      ((IGameAI) ai).DetermineAction(_state);

      Console.WriteLine(
        $"Evaluated {ai.LatestLogInfo.nodesEvaluated} end states in {ai.LatestLogInfo.elapsedTime.TotalSeconds} seconds.");
      Console.WriteLine(
        $"{ai.LatestLogInfo.successfulHashMapLookups} successful hash map lookups and {ai.LatestLogInfo.prunedPaths} paths pruned.");
    }

    [Test]
    [Explicit]
    public void FullGameRun()
    {
      var logInfo = RunSingleGame(4, SearchDepthConfiguration.actions);

      Console.WriteLine(
        $"Evaluated {logInfo.Sum(l => l.nodesEvaluated)} end states over {logInfo.Count()} calls in {logInfo.Sum(l => l.elapsedTime.TotalSeconds)} seconds.");
      Console.WriteLine(
        $"{logInfo.Sum(l => l.successfulHashMapLookups)} successful hash map lookups and {logInfo.Sum(l => l.prunedPaths)} paths pruned.");
    }

    [Test]
    [Explicit]
    public void RunGameSample()
    {
      int numberOfGames = 10;
      var runTimes = new List<TimeSpan>();

      for (int i = 0; i < numberOfGames; i++)
      {
        runTimes.Add(RunSingleGame(4, SearchDepthConfiguration.actions).Select(l => l.elapsedTime).Total());
      }
      
      Console.WriteLine($"{numberOfGames} evaluated in {runTimes.Total()}");
      Console.WriteLine("//Excluding first run");
      var reducedRunTimes = runTimes.GetRange(1, numberOfGames - 1);
      Console.WriteLine($"Average runtime: {reducedRunTimes.Average()}");
      Console.WriteLine($"Fastest run: {reducedRunTimes.Min()})");
      Console.WriteLine($"Slowest run: {reducedRunTimes.Max()})");
      Console.WriteLine();

      for (int i = 1; i <= runTimes.Count(); i++)
      {
        Console.WriteLine($"{i}: {runTimes[i - 1]}");
      }
    }

    IEnumerable<LogInfo> RunSingleGame(int depth, SearchDepthConfiguration searchDepthConfiguration)
    {
      _state = SetupStartState();
      var ai = new MinimaxAI(new Evaluator(), depth, searchDepthConfiguration, AIMethods.SearchConfiguration.NoRestrictions, AIMethods.LoggingConfiguration.LogAll);

      var playerTurn = _state.PlayerTurn;
      while (!_state.IsGameOver)
      {
        var moves = ((IGameAI) ai).DetermineAction(_state);
        while (!_state.IsGameOver && _state.PlayerTurn == playerTurn && moves.Length > 0)
        {
          _state = (IState) _state.Actions()[moves[0]].DoCoreAction();
          moves = moves.Skip(1).ToArray();
        }

        playerTurn = playerTurn.Other();
      }

      return ai.logInfos;
    }

    // Log
    // Commit 2a66ec65 depth 4 actions
    
    // Evaluated 178971 end states over 54 calls in 76,89423450000001 seconds.
    // 0 successful hash map lookups and 5881 paths pruned.
    
    // Evaluated 264535 end states over 60 calls in 65,9268776 seconds.
    // 0 successful hash map lookups and 7368 paths pruned.

    // Commit a4b57123 depth 4 actions
    // Evaluated 651991 end states over 68 calls in 46,986610899999995 seconds.
    // 21650 successful hash map lookups and 10086 paths pruned.
    internal static ImmutableState SetupStartState()
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return StateFactory.Initiate(player1Deck, player2Deck);
    }
  }
}