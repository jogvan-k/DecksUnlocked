using UnlockedCore.AITypes;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleGames;
using KeyforgeUnlockedConsole.Decks;
using UnlockedCore;

namespace KeyforgeUnlockedConsole
{
  public static class Program
  {
    static void Main(string[] args)
    {
      var consoleGame = PlayerVsAiGame();
      consoleGame.StartGame();
    }

    static IConsoleGame PlayerVsAiGame()
    {
      var player1Deck = DeckLoader.LoadDeck("");
      var player2Deck = DeckLoader.LoadDeck("");
      return new PlayerVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), 2, SearchDepthConfiguration.turn, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll), Player.Player1);
    }
    static IConsoleGame TwoPlayerGame()
    {
      var player1Deck = DeckLoader.LoadDeck("");
      var player2Deck = DeckLoader.LoadDeck("");
      return new TwoPlayerGame(StateFactory.Initiate(player1Deck, player2Deck));
    }

    static IConsoleGame AiVsAiGame()
    {
      
      var player1Deck = DeckLoader.LoadDeck("");
      var player2Deck = DeckLoader.LoadDeck("");
      return new AIVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), 3, SearchDepthConfiguration.turn, SearchConfiguration.NoRestrictions, LoggingConfiguration.LogAll));
    }
  }
}