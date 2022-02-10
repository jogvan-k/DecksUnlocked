using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
    public sealed class DiscardCard : EffectWithCard<DiscardCard>
    {
        public DiscardCard(ICard card) : base(card)
        {
        }

        protected override void ResolveImpl(IMutableState state)
        {
            if (!state.Hands[state.PlayerTurn].Remove(Card))
                throw new CardNotPresentException(state, Card);
            state.Discards[state.PlayerTurn].Add(Card);
            state.ResolvedEffects.Add(new CardDiscarded(Card));
            state.HistoricData.CardsDiscardedThisTurn = state.HistoricData.CardsDiscardedThisTurn.Add(Card);
        }
    }
}