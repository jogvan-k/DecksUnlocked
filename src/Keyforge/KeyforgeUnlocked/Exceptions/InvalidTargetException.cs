using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
    public sealed class InvalidTargetException : KeyforgeUnlockedException
    {
        public readonly IIdentifiable TargetId;

        public InvalidTargetException(IState state, IIdentifiable targetId) : base(state)
        {
            TargetId = targetId;
        }
    }
}