using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedConsole.ConsoleGames;
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
      var ai = new MinimaxAI(new Evaluator(), 2, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogAll);

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
      _state = SetupStartState();
      var ai = new MinimaxAI(new Evaluator(), 4, SearchDepthConfiguration.actions, AIMethods.LoggingConfiguration.LogAll);

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

      Console.WriteLine(
        $"Evaluated {ai.logInfos.Sum(l => l.nodesEvaluated)} end states over {ai.logInfos.Length} calls in {ai.logInfos.Sum(l => l.elapsedTime.TotalSeconds)} seconds.");
      Console.WriteLine(
        $"{ai.logInfos.Sum(l => l.successfulHashMapLookups)} successful hash map lookups and {ai.logInfos.Sum(l => l.prunedPaths)} paths pruned.");
    }

    ImmutableState SetupStartState()
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return StateFactory.Initiate(player1Deck, player2Deck);
    }
  }
}