using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class InvalidTargetException : KeyforgeUnlockedException
  {
    public readonly string TargetId;

    public InvalidTargetException(IState state, string targetId) : base(state)
    {
      TargetId = targetId;
    }
  }
}