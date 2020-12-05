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
      var result = RunSingleGame(4, SearchDepthConfiguration.actions);

      Console.WriteLine(
        $"Evaluated {result.Item1.Sum(l => l.nodesEvaluated)} end states over {result.logInfos.Count()} calls in {result.logInfos.Sum(l => l.elapsedTime.TotalSeconds)} seconds.");
      Console.WriteLine(
        $"{result.Item1.Sum(l => l.successfulHashMapLookups)} successful hash map lookups and {result.logInfos.Sum(l => l.prunedPaths)} paths pruned.");
    }

    [Test]
    [Explicit]
    public void RunGameSample()
    {
      int numberOfGames = 10;
      var runTimes = new List<(TimeSpan, int)>();

      for (int i = 0; i < numberOfGames; i++)
      {
        var results = RunSingleGame(4, SearchDepthConfiguration.actions);
        runTimes.Add((results.logInfos.Select(l => l.elapsedTime).Total(), results.turns));
      }
      
      Console.WriteLine($"{numberOfGames} evaluated in {runTimes.Select(r => r.Item1).Total()}");
      if (numberOfGames < 1)
        return;
      
      Console.WriteLine("//Excluding first run");
      
      var reducedRunTimes = runTimes.GetRange(1, numberOfGames - 1);
      Console.WriteLine($"Average runtime: {reducedRunTimes.Select(r => r.Item1).Average()}");
      Console.WriteLine($"Fastest run: {reducedRunTimes.Select(r => r.Item1).Min()})");
      Console.WriteLine($"Slowest run: {reducedRunTimes.Select(r => r.Item1).Max()})");
      Console.WriteLine();

      for (int i = 1; i <= runTimes.Count(); i++)
      {
        Console.WriteLine($"{i}: {runTimes[i - 1].Item1}, turns: {runTimes[i - 1].Item2}");
      }
    }
    
    // 10 evaluated in 00:06:20.9513549
       //Excluding first run
    // Average runtime: 00:00:37.5480825
    // Fastest run: 00:00:25.9903145)
    // Slowest run: 00:00:54.4068430)

    // 1: 00:00:43.0186122, turns: 26
    // 2: 00:00:26.8844595, turns: 25
    // 3: 00:00:37.9773853, turns: 24
    // 4: 00:00:34.2471220, turns: 28
    // 5: 00:00:31.9901223, turns: 24
    // 6: 00:00:54.4068430, turns: 31
    // 7: 00:00:42.5163147, turns: 27
    // 8: 00:00:42.2375371, turns: 27
    // 9: 00:00:25.9903145, turns: 25
    // 10: 00:00:41.6826443, turns: 27


    (IEnumerable<LogInfo> logInfos, int turns) RunSingleGame(int depth, SearchDepthConfiguration searchDepthConfiguration)
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

      return (ai.logInfos, _state.TurnNumber);
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