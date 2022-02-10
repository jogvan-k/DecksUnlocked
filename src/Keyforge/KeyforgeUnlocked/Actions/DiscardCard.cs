using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
    public sealed class DiscardCard : BasicActionWithCard<DiscardCard>
    {
        public DiscardCard(ImmutableState origin, ICard card) : base(origin, card)
        {
        }

        protected override void DoSpecificActionNoResolve(IMutableState state)
        {
            state.Effects.Push(new Effects.DiscardCard(Card));
        }

        public override string ToString()
        {
            return $"Discard card";
        }
    }
}