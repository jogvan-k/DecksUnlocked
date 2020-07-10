using UnlockedCore.States;

namespace UnlockedCore.Actions
{
  public interface ICoreAction
  {
    ICoreState DoCoreAction(ICoreState state);
  }
}