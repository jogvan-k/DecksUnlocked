using UnlockedCore;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public class DiscardShuffledIntoDeck : ResolvedEffectWithPlayer<DiscardShuffledIntoDeck>
    {
        public DiscardShuffledIntoDeck(Player player) : base(player)
        {
        }

        public override string ToString()
        {
            return $"{Player}'s deck shuffled";
        }
    }
}