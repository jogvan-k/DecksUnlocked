using System;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public class KeyforgeUnlockedException : Exception
    {
        public IState State { get; }

        public KeyforgeUnlockedException(IState state)
        {
            State = state;
        }

        protected bool Equals(KeyforgeUnlockedException other)
        {
            return Equals(State, other.State);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyforgeUnlockedException)obj);
        }

        public override int GetHashCode()
        {
            return State.GetHashCode();
        }
    }
}