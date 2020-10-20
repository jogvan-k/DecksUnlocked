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
    MinimaxAI ai;
    [Test]
    public void MinimaxAIWithAndWithoutHashtableLookupYieldSameResult()
    {
      var state = BenchmarkTest.SetupStartState();
      ai = new MinimaxAI(new Evaluator(), 3, SearchDepthConfiguration.turn, SearchConfiguration.NoHashTable, LoggingConfiguration.LogSuccessfulHashMapLookup);
      var noHashMapResult = ((IGameAI)ai).DetermineAction(state);
      
      Assert.That(ai.LatestLogInfo.successfulHashMapLookups, Is.EqualTo(0));
      
      ai = new MinimaxAI(new Evaluator(), 3, SearchDepthConfiguration.turn, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogSuccessfulHashMapLookup);
      
      var hashMapResult = ((IGameAI)ai).DetermineAction(state);

      var successfulHashMapLookups = ai.LatestLogInfo.successfulHashMapLookups;
      Console.WriteLine($"{successfulHashMapLookups} successful hash map lookups");
      Assert.That(successfulHashMapLookups, Is.GreaterThan(0));

      Assert.That(hashMapResult, Is.EqualTo(noHashMapResult));
    }
  }
}