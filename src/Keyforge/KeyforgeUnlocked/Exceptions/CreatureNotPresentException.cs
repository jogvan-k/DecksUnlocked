using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
    public class CreatureNotPresentException : KeyforgeUnlockedException
    {
        public IIdentifiable Id;

        public CreatureNotPresentException(IState state, IIdentifiable id) : base(state)
        {
            Id = id;
        }
    }
}