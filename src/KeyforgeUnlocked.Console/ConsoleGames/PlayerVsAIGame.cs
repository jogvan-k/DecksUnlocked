using System;
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
        _state = (IState) _gameAi.DetermineAction(_state).DoCoreAction();
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