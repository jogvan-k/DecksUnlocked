using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.AITypes;

namespace KeyforgeUnlocked.IntegrationTest.Benchmark
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
    public void NegamaxAIRun()
    {
      _state = SetupStartState();
      var ai = new NegamaxAI(new Evaluator(), searchLimit.NewTurn(2, searchTime.NewSeconds(15)), SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll);

      ((IGameAI) ai).DetermineAction(_state);

      Console.WriteLine(
        $"Evaluated {ai.LatestLogInfo.endNodesEvaluated} end states in {ai.LatestLogInfo.elapsedTime.TotalSeconds} seconds.");
      Console.WriteLine(
        $"{ai.LatestLogInfo.successfulHashMapLookups} successful hash map lookups and {ai.LatestLogInfo.prunedPaths} paths pruned.");
    }

    [Test]
    [Explicit]
    public void FullGameRun()
    {
      var result = RunSingleGame(searchLimit.NewTurn(1, searchTime.NewSeconds(15)));

      Console.WriteLine(
        $"Evaluated {result.Item1.Sum(l => l.endNodesEvaluated)} end states over {result.logInfos.Count()} calls and {result.turns} turns in {result.logInfos.Sum(l => l.elapsedTime.TotalSeconds)} seconds.");
      Console.WriteLine(
        $"{result.Item1.Sum(l => l.successfulHashMapLookups)} successful hash map lookups and {result.logInfos.Sum(l => l.prunedPaths)} paths pruned.");
    }
    
    // Evaluated 745853 end states over 19 calls and 20 turns in 383,9004011000002 seconds.
    // 4435705 successful hash map lookups and 0 paths pruned.
    
    // Evaluated 774770 end states over 19 calls and 20 turns in 428,71984790000016 seconds.
    // 4608404 successful hash map lookups and 0 paths pruned.
    
    // 3 turns search depth
    // Evaluated 1430498 end states over 39 calls and 40 turns in 245,0023509 seconds.
    // 2407768 successful hash map lookups and 613184 paths pruned.
    
    // 1 turn search depth /w 5 s search time
    // Evaluated 76924 end states over 9 calls and 10 turns in 38,1994761 seconds.
    //110604 successful hash map lookups and 0 paths pruned.

    [Test]
    [Explicit]
    public void RunGameSample()
    {
      var numberOfGames = 10;
      var runTimes = new List<(TimeSpan, int)>();
      var moves = new List<int[]>();

      for (int i = 0; i < numberOfGames; i++)
      {
        var results = RunSingleGame(searchLimit.NewTurn(3, searchTime.Unlimited));
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
    
//       2 turn evaluation
//       10 evaluated in 00:00:25.3878356
//       //Excluding first run
//       Average runtime: 00:00:02.4998651
//       Fastest run: 00:00:02.4656404)
//       Slowest run: 00:00:02.5894956)
//      
//       1: 00:00:02.8890493, turns: 40
//       2: 00:00:02.5149706, turns: 40
//       3: 00:00:02.4882552, turns: 40
//       4: 00:00:02.4833789, turns: 40
//       5: 00:00:02.4656404, turns: 40
//       6: 00:00:02.4706355, turns: 40
//       7: 00:00:02.5894956, turns: 40
//       8: 00:00:02.4828141, turns: 40
//       9: 00:00:02.5221628, turns: 40
//       10: 00:00:02.4814332, turns: 40

//       3 turn evaluation
//       10 evaluated in 00:43:29.3554156
//       //Excluding first run
//       Average runtime: 00:04:20.9823475
//       Fastest run: 00:04:18.0877844)
//       Slowest run: 00:04:27.3948984)
//      
//       1: 00:04:20.5142882, turns: 34
//       2: 00:04:22.4435152, turns: 34
//       3: 00:04:19.6126273, turns: 34
//       4: 00:04:27.3948984, turns: 34
//       5: 00:04:18.0877844, turns: 34
//       6: 00:04:18.9856982, turns: 34
//       7: 00:04:23.0101861, turns: 34
//       8: 00:04:19.5938859, turns: 34
//       9: 00:04:19.8103471, turns: 34
//       10: 00:04:19.9021848, turns: 34


    (IEnumerable<LogInfo> logInfos, int turns, int[] movesTaken) RunSingleGame(searchLimit depth)
    {
      _state = SetupStartState();
      var ai = new NegamaxAI(new Evaluator(), depth, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll);
      var moves = new int[0];
      var movesTaken = Enumerable.Empty<int>();

      var playerTurn = _state.PlayerTurn;
      while (!_state.IsGameOver)
      {
        moves = ((IGameAIWithVariationPath) ai).DetermineActionWithVariation(_state, moves);
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
      var player1Deck = Deck.LoadDeckFromFile(Assembly.Load("KeyforgeUnlocked.Cards"),"Fyre, Bareleyhill Bodyguard.txt");
      var player2Deck = Deck.LoadDeckFromFile(Assembly.Load("KeyforgeUnlocked.Cards"), "Fyre, Bareleyhill Bodyguard.txt");
      return StateFactory.Initiate(player1Deck, player2Deck);
    }
  }
}