using System;
using System.Collections.Generic;
using UnlockedCore.Actions;

namespace UnlockedCore.States
{
  public interface CoreState
  {
    Player PlayerTurn { get; }
    int TurnNumber { get; }
    bool IsGameOver { get; }
    List<CoreAction> Actions();
  }
}