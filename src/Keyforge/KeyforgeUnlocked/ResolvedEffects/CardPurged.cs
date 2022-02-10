using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public class CardPurged : ResolvedEffectWithIdentifiable<CardPurged>
    {
        public CardPurged(IIdentifiable id) : base(id)
        {
        }

        public override string ToString()
        {
            return $"{Id.Name} purged";
        }
    }
}