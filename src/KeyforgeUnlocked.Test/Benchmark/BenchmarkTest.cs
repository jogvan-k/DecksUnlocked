using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;  
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.AI;
using UnlockedCore.AITypes;

namespace KeyforgeUnlockedTest.Benchmark
{
  [TestFixture]
  sealed class BenchmarkTest
  {
    readonly IState _startState = SetupStartState();
    IState _state;

    [Test]
    [Explicit]
    public void StateRun()
    {
      var i = 0;
      _state = _startState;

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
    public void NegamaxAIRun()
    {
      _state = _startState;
      var ai = new NegamaxAI(new Evaluator(), 2, SearchDepthConfiguration.turn, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll);

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
      var result = RunSingleGame(2, SearchDepthConfiguration.turn);

      Console.WriteLine(
        $"Evaluated {result.Item1.Sum(l => l.nodesEvaluated)} end states over {result.logInfos.Count()} calls and {result.turns} turns in {result.logInfos.Sum(l => l.elapsedTime.TotalSeconds)} seconds.");
      Console.WriteLine(
        $"{result.Item1.Sum(l => l.successfulHashMapLookups)} successful hash map lookups and {result.logInfos.Sum(l => l.prunedPaths)} paths pruned.");
    }
    
    // 3 turns search depth
    // Evaluated 1430498 end states over 39 calls and 40 turns in 245,0023509 seconds.
    // 2407768 successful hash map lookups and 613184 paths pruned.

    [Test]
    [Explicit]
    public void RunGameSample()
    {
      int numberOfGames = 2;
      var runTimes = new List<(TimeSpan, int)>();
      var moves = new List<int[]>();

      for (int i = 0; i < numberOfGames; i++)
      {
        var results = RunSingleGame(3, SearchDepthConfiguration.turn);
        runTimes.Add((results.logInfos.Select(l => l.elapsedTime).Total(), results.turns));
        moves.Add(results.movesTaken);
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
        //Console.WriteLine($"Moves: " + moves[i - 1].Select(m => m.ToString()).Aggregate((f, s) => f + ',' + s));
      }
    }
    
//     10 evaluated in 00:00:27.9838499
//     //Excluding first run
//     Average runtime: 00:00:02.7561327
//     Fastest run: 00:00:02.7282601)
//     Slowest run: 00:00:02.7739614)
//
//     1: 00:00:03.1786552, turns: 40
//     2: 00:00:02.7711728, turns: 40
//     3: 00:00:02.7634663, turns: 40
//     4: 00:00:02.7739614, turns: 40
//     5: 00:00:02.7544971, turns: 40
//     6: 00:00:02.7619887, turns: 40
//     7: 00:00:02.7660368, turns: 40
//     8: 00:00:02.7282601, turns: 40
//     9: 00:00:02.7412833, turns: 40
//     10: 00:00:02.7445282, turns: 40




    (IEnumerable<LogInfo> logInfos, int turns, int[] movesTaken) RunSingleGame(int depth, SearchDepthConfiguration searchDepthConfiguration)
    {
      _state = _startState;
      var ai = new NegamaxAI(new Evaluator(), depth, searchDepthConfiguration, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll);
      var movesTaken = Enumerable.Empty<int>();

      var playerTurn = _state.PlayerTurn;
      while (!_state.IsGameOver)
      {
        var moves = ((IGameAI) ai).DetermineAction(_state);
        while (!_state.IsGameOver && _state.PlayerTurn == playerTurn && moves.Length > 0)
        {
          var move = moves[0];
          _state = (IState) _state.Actions()[move].DoCoreAction();
          moves = moves.Skip(1).ToArray();
          movesTaken = movesTaken.Append(move);
        }

        playerTurn = playerTurn.Other();
      }

      return (ai.logInfos, _state.TurnNumber, movesTaken.ToArray());
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