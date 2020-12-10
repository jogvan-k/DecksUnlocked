using UnlockedCore.AITypes;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedConsole.ConsoleGames;
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
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return new PlayerVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), 2, SearchDepthConfiguration.turn, AIMethods.SearchConfiguration.NoRestrictions, AIMethods.LoggingConfiguration.LogAll), Player.Player1);
    }
    static IConsoleGame TwoPlayerGame()
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return new TwoPlayerGame(StateFactory.Initiate(player1Deck, player2Deck));
    }

    static IConsoleGame AiVsAiGame()
    {
      
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return new AIVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new NegamaxAI(new Evaluator(), 3, SearchDepthConfiguration.turn, AIMethods.SearchConfiguration.NoRestrictions, AIMethods.LoggingConfiguration.LogAll));
    }
  }
}