using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public sealed class ArchivedClaimed : Equatable<ArchivedClaimed>, IResolvedEffect
    {
        public override string ToString()
        {
            return "Archive claimed";
        }
    }
}