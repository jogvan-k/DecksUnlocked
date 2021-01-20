using System;
using System.Reflection;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using NUnit.Framework;
using UnlockedCore;
using UnlockedCore.MCTS;

namespace KeyforgeUnlocked.IntegrationTest.Benchmark
{
  [TestFixture]
  sealed class MCSTBenchamrkTest
  {
    [Test]
    [Explicit]
    public void NumberOfSimulations()
    {
      var root = SetupStartState();
      var ai = new UnlockedCore.MCTS.AI.MonteCarloTreeSearch(searchTime.NewSeconds(10), UnlockedCore.MCTS.AI.configuration.TranspositionTable);
      
      ((IGameAI) ai).DetermineAction(root);
      
      Console.WriteLine($"Ran {ai.LatestLogInfo().simulations} simulations in {ai.LatestLogInfo().elapsedTime.Seconds} seconds.");
    }

  internal static ImmutableState SetupStartState()
    {
      var player1Deck = Deck.LoadDeckFromFile(Assembly.Load("KeyforgeUnlocked.Cards"),"Fyre, Bareleyhill Bodyguard.txt");
      var player2Deck = Deck.LoadDeckFromFile(Assembly.Load("KeyforgeUnlocked.Cards"), "Fyre, Bareleyhill Bodyguard.txt");
      return StateFactory.Initiate(player1Deck, player2Deck);
    }
  }
}