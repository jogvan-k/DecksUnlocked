using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
    public class ArtifactNotPresentException : KeyforgeUnlockedException
    {
        public readonly IIdentifiable Id;

        public ArtifactNotPresentException(IState state, IIdentifiable id) : base(state)
        {
            Id = id;
        }
    }
}