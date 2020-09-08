using System;
using System.Diagnostics;
using System.Linq;
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
    ICoreState _state;
    [Test]
    [Explicit]
    public void StateRun()
    {
      var i = 0;
      _state = SetupStartState();

      var stopwatch = Stopwatch.StartNew();
      while (!((IState)_state).IsGameOver && ++i < 50000)
      {
        _state = _state.Actions().First().DoCoreAction();
      }
      stopwatch.Stop();
      Console.WriteLine($"Ran through {i} states in {stopwatch.Elapsed.TotalSeconds} seconds.");
    }

    [Test]
    [Explicit]
    public void MinimaxAIRun()
    {
      _state = SetupStartState();
      var ai = new MinimaxAI(new Evaluator(), 3, SearchDepthConfiguration.turn, AIMethods.LoggingConfiguration.LogAll);

      ((IGameAI) ai).DetermineAction(_state);
      
      Console.WriteLine($"Evaluated {ai.logInfo.nodesEvaluated} end states in {ai.logInfo.elapsedTime.TotalSeconds} seconds.");
      Console.WriteLine($"{ai.logInfo.successfulHashMapLookups} successful hash map lookups and {ai.logInfo.prunedPaths} paths pruned.");
    }

    ImmutableState SetupStartState()
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return StateFactory.Initiate(player1Deck, player2Deck);
    }
  }
}