using System;

namespace KeyforgeUnlocked.Exceptions
{
  public class KeyforgeUnlockedException : Exception
  {
    public KeyforgeUnlockedException(State state)
    {
      this.state = state;
    }

    State state { get; }
  }
}