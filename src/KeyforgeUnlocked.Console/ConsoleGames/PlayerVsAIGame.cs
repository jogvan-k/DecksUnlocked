using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
  public sealed class PlayerVsAIGame : BaseConsoleGame
  {
    IGameAI _gameAi;
    Player _playingPlayer;

    public PlayerVsAIGame(IState state, IGameAI gameAi, Player playingPlayer, LogInfo logInfo = LogInfo.None) : base(state, logInfo)
    {
      _gameAi = gameAi;
      _playingPlayer = playingPlayer;
    }

    protected override void AdvanceState()
    {
      if (_state.PlayerTurn == _playingPlayer)
      {
        AdvanceStateOnPlayerTurn();
      }
      else
      {
        AdvanceStateOnAITurn(_playingPlayer.Other(), _gameAi);
      }
    }
  }
}