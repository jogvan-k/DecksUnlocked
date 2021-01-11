using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleGames;
using KeyforgeUnlockedConsole.Decks;
using UnlockedCore;
using UnlockedCore.AITypes;
using LogInfo = KeyforgeUnlockedConsole.ConsoleGames.LogInfo;

namespace KeyforgeUnlockedConsole
{
  public static class Program
  {
    static void Main(string[] args)
    {
      Console.SetWindowSize(200, 50);
      var consoleGame = PlayerVsAiGame();
      consoleGame.StartGame();
    }

    static IConsoleGame PlayerVsAiGame()
    {
      var player1Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      var player2Deck = DeckLoader.LoadDeck("Fyre, Bareleyhill Bodyguard.txt");
      return new PlayerVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(),  searchLimit.NewTurn(2, searchTime.NewSeconds(10)), SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll), Player.Player1, logInfo: LogInfo.CalculationInfo);
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
      return new AIVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), searchLimit.NewTurn(3, searchTime.NewSeconds(5)), SearchConfiguration.IncrementalSearch, LoggingConfiguration.LogAll));
    }
  }
}