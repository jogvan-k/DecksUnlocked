using ClassLibrary1.AITypes;
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
      return new PlayerVsAIGame(StateFactory.Initiate(player1Deck, player2Deck), new MinimaxAI(new Evaluator(), 5), Player.Player1);
    }
    static IConsoleGame TwoPlayerGame()
    {
      var player1Deck = Deck.LoadDeckFromFile("");
      var player2Deck = Deck.LoadDeckFromFile("");
      return new TwoPlayerGame(StateFactory.Initiate(player1Deck, player2Deck));
    }
  }
}