using System;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using UnlockedCore;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
  public sealed class PlayerVsAIGame : BaseConsoleGame
  {
    IGameAI _gameAi;
    Player _playingPlayer;

    public PlayerVsAIGame(IState state, IGameAI gameAi, Player playingPlayer) : base(state)
    {
      _gameAi = gameAi;
      _playingPlayer = playingPlayer;
    }

    protected override void AdvanceState()
    {
      Console.Clear();
      if (_state.PlayerTurn == _playingPlayer)
      {
        AdvanceStateOnPlayerTurn();
      }
      else
      {
        var aiMoves = Array.Empty<int>();
        while (_state.PlayerTurn != _playingPlayer)
        {
          if (aiMoves.Length == 0)
            aiMoves = _gameAi.DetermineAction(_state);
          _state = (IState) _state.Actions()[aiMoves[0]].DoCoreAction();
          aiMoves = aiMoves.Skip(1).ToArray();
          _state.PrintAITurn();
          if (_state.PlayerTurn != _playingPlayer)
          {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
          }
        }
      }
    }
  }
}