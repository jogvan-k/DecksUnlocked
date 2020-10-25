using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Benchmark;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.AITypes;
using static AIMethods;

namespace KeyforgeUnlockedTest.AI
{
  [TestFixture]
  sealed class HashLookupTests
  {
    MinimaxAI noHashTableAi;
    MinimaxAI hashTableAi;
    SearchDepthConfiguration _searchDepthConfiguration = SearchDepthConfiguration.turn;
    static readonly int Depth = 2;
    IState state = BenchmarkTest.SetupStartState();

    int[] noHashMapResult, hashMapResult;

    [Test]
    [Explicit]
    public void MinimaxAIWithAndWithoutHashtableLookupYieldSameResult()
    {
      noHashTableAi = new MinimaxAI(new Evaluator(), Depth, _searchDepthConfiguration, SearchConfiguration.NoHashTable, LoggingConfiguration.LogSuccessfulHashMapLookup);
      hashTableAi = new MinimaxAI(new Evaluator(), Depth, _searchDepthConfiguration, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogSuccessfulHashMapLookup);

      while (!state.IsGameOver)
      {
        noHashMapResult = ((IGameAI)noHashTableAi).DetermineAction(state);
        hashMapResult = ((IGameAI) hashTableAi).DetermineAction(state);

        AssertAndWriteFindings();

        var currentTurn = state.TurnNumber;
        for (int i = 0; state.TurnNumber == currentTurn; i++)
          state = (IState) state.Actions()[hashMapResult[i]].DoCoreAction();
      }
    }

    void AssertAndWriteFindings()
    {
      Console.WriteLine($"Turn {state.TurnNumber}");
      Console.WriteLine($"{hashTableAi.LatestLogInfo.successfulHashMapLookups} successful hash table lookups");
      
      Assert.That(noHashTableAi.LatestLogInfo.successfulHashMapLookups, Is.EqualTo(0));
      var successfulHashMapLookups = hashTableAi.LatestLogInfo.successfulHashMapLookups;
      Assert.That(successfulHashMapLookups, Is.GreaterThan(0));
      Assert.That(hashMapResult, Is.EqualTo(noHashMapResult));
    }
  }
}