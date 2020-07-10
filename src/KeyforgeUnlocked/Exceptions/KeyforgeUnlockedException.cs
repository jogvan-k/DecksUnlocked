using System;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class KeyforgeUnlockedException : Exception
  {
    public KeyforgeUnlockedException(IState state)
    {
      this.State = state;
    }

    public IState State { get; }
  }
}