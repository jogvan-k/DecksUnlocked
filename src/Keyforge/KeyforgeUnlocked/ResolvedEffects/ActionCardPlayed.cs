using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public sealed class ActionCardPlayed : ResolvedEffectWithCard<ActionCardPlayed>
    {
        public ActionCardPlayed(ICard card) : base(card)
        {
        }

        public override string ToString()
        {
            return $"{_card.Name} played";
        }
    }
}