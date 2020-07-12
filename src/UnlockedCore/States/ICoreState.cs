using System;
using System.Collections.Generic;
using UnlockedCore.Actions;

namespace UnlockedCore.States
{
  public interface ICoreState
  {
    Player PlayerTurn { get; }
    int TurnNumber { get; }
    bool IsGameOver { get; }
    IList<ICoreAction> Actions();
  }
}