using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
    public class NoCallbackException : KeyforgeUnlockedException
    {
        public readonly IIdentifiable Id;

        public NoCallbackException(IState state, IIdentifiable id) : base(state)
        {
            Id = id;
        }
    }
}