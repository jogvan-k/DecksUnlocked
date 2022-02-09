using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  /// <summary>
  /// Thrown when a base action is executed while there are still unresolved effects.
  /// </summary>
  public sealed class UnresolvedEffectsException : KeyforgeUnlockedException
  {
    public UnresolvedEffectsException(IState state) : base(state)
    {
    }
  }
}