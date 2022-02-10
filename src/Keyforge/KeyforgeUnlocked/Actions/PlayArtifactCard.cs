using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
    public class PlayArtifactCard : BasicActionWithCard<PlayArtifactCard>
    {
        public PlayArtifactCard(ImmutableState origin, ICard card) : base(origin, card)
        {
        }

        protected override void DoSpecificActionNoResolve(IMutableState state)
        {
            if (!state.Hands[state.PlayerTurn].Remove(Card))
                throw new CardNotPresentException(state, Card);

            state.Effects.Push(new Effects.PlayArtifactCard((IArtifactCard)Card));
        }

        public override string ToString()
        {
            return $"Play artifact card";
        }
    }
}