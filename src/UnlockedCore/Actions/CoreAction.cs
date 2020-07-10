using UnlockedCore.States;

namespace UnlockedCore.Actions
{
  public interface CoreAction
  {
    CoreState DoCoreAction(CoreState state);
  }
}