using System;
using KeyforgeUnlocked.IntegrationTest.Benchmark;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.AI;
using UnlockedCore.AITypes;

namespace KeyforgeUnlocked.IntegrationTest.AI
{
  [TestFixture]
  sealed class HashLookupTests
  {
    NegamaxAI noHashTableAi;
    NegamaxAI hashTableAi;
    static readonly MinimaxTypes.searchLimit Depth = MinimaxTypes.searchLimit.NewTurn(3, searchTime.Unlimited);
    IState state = BenchmarkTest.SetupStartState();

    int[] noHashMapResult, hashMapResult;

    [Test]
    [Explicit]
    public void NegamaxAIWithAndWithoutHashtableLookupYieldSameResult()
    {
      noHashTableAi = new NegamaxAI(new Evaluator(), Depth, MinimaxTypes.SearchConfiguration.NoHashTable, MinimaxTypes.LoggingConfiguration.LogSuccessfulHashMapLookup);
      hashTableAi = new NegamaxAI(new Evaluator(), Depth, MinimaxTypes.SearchConfiguration.NoRestrictions, MinimaxTypes.LoggingConfiguration.LogSuccessfulHashMapLookup);

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