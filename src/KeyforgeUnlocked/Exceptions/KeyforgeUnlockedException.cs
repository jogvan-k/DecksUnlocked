using System;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class KeyforgeUnlockedException : Exception
  {
    public KeyforgeUnlockedException(IState state)
    {
      this.state = state;
    }

    IState state { get; }
  }
}