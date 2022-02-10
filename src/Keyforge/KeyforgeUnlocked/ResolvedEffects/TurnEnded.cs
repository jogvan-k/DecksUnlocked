using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public sealed class TurnEnded : Equatable<TurnEnded>, IResolvedEffect
    {
        public override string ToString()
        {
            return $"Turn ended";
        }
    }
}