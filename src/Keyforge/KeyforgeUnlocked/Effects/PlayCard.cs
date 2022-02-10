using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Effects
{
    public abstract class PlayCard<T> : EffectWithCard<T> where T : PlayCard<T>
    {
        protected void ResolvePlayEffects(IMutableState state)
        {
            foreach (var pip in Card.CardPips)
            {
                state.ResolvePip(pip);
            }

            state.RaiseEvent(EventType.CardPlayed, Card, state.PlayerTurn);
            Card.CardPlayAbility?.Invoke(state, Card, state.PlayerTurn);
        }

        protected PlayCard(ICard card) : base(card)
        {
        }
    }
}