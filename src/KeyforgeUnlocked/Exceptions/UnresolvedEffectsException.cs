namespace KeyforgeUnlocked.Exceptions
{
  /// <summary>
  /// Thrown when a base action is executed while there are still unresolved effects.
  /// </summary>
  public class UnresolvedEffectsException : KeyforgeUnlockedException
  {
    public UnresolvedEffectsException(State state) : base(state){}
  }
}