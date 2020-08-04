using UnlockedCore.Actions;
using UnlockedCore.States;

namespace UnlockedCore.AI
{
  public interface IGameAI
  {
    ICoreAction DetermineAction(ICoreState state);
  }
}