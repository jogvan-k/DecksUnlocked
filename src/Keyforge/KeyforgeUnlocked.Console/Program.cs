using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleGames;
using KeyforgeUnlockedConsole.Decks;
using UnlockedCore;
using UnlockedCore.AI;
using UnlockedCore.AITypes;
using UnlockedCore.MCTS;
using LogInfo = KeyforgeUnlockedConsole.ConsoleGames.LogInfo;

namespace KeyforgeUnlockedConsole
{
  public static class Program
  {
    static void Main(string[] args)
    {
      //Console.SetWindowSize(200, 50);
      var consoleGame = PlayerVsAiGame();
      consoleGame.StartGame();
    }

    static IConsoleGame PlayerVsAiGame()
    {
      var player1Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      var player2Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      return new PlayerVsAIGame(StateFactory.Initiate(player1Deck, player2Deck),
        new ServerAI(), Player.Player1);
      //new AI.MonteCarloTreeSearch(searchTime.NewSeconds(4), 1000000, AI.configuration.All), Player.Player1, LogInfo.None);
    }
    static IConsoleGame TwoPlayerGame()
    {
      var player1Deck = DeckLoader.LoadDeck("");
      var player2Deck = DeckLoader.LoadDeck("");
      return new TwoPlayerGame(StateFactory.Initiate(player1Deck, player2Deck));
    }

    static IConsoleGame AiVsAiGame()
    {
      
      var player1Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      var player2Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      return new AIVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), MinimaxTypes.searchLimit.NewTurn(3, searchTime.NewSeconds(5)), MinimaxTypes.SearchConfiguration.IncrementalSearch, MinimaxTypes.LoggingConfiguration.LogAll));
    }
  }
}