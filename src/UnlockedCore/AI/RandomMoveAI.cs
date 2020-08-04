using System;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace UnlockedCore.AI
{
  public sealed class RandomMoveAI : IGameAI
  {
    static Random rng = new Random();

    public ICoreAction DetermineAction(ICoreState state)
    {
      var actionCount = state.Actions().Count;
      return actionCount == 0 ? null : state.Actions()[rng.Next() % actionCount];
    }
  }
}