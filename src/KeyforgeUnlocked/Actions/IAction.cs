using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Actions
{
  public interface IAction : ICoreAction
  {
    /// <summary>
    /// Gives a string-based identity of the action. Used to compare actions to ensure the same ordering. The identity
    /// should therefore be different from action's identity of the same type for a given <see cref="Origin"/>.
    /// </summary>
    /// <returns></returns>
    string Identity();
    
    IState DoAction(IState state);
  }
}