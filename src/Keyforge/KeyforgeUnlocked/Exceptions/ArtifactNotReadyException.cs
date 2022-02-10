using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
    public class ArtifactNotReadyException : KeyforgeUnlockedException
    {
        public IIdentifiable Artifact;

        public ArtifactNotReadyException(IState state, IIdentifiable artifact) : base(state)
        {
            Artifact = artifact;
        }
    }
}