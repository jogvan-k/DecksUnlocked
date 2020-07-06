using UnlockedCore.States;

namespace UnlockedCore.Actions
{
  public interface CoreAction
  {
    public CoreState CoreState { get; }
    CoreState DoCoreAction();
  }
}