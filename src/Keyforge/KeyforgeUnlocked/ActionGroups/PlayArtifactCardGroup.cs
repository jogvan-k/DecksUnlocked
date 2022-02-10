using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public sealed class PlayArtifactCardGroup : PlayCardGroup<PlayArtifactCardGroup>
    {
        public new IArtifactCard Card => (IArtifactCard)base.Card;

        public PlayArtifactCardGroup(IArtifactCard card) : base(card)
        {
        }

        protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
        {
            var playAction = new PlayArtifactCard(origin, Card);
            var discardAction = new DiscardCard(origin, Card);

            if (Card.CardPlayAllowed(origin, playAction))
                return ImmutableList.Create<IAction>(playAction, discardAction);
            return ImmutableList.Create<IAction>(discardAction);
        }

        public override string ToString()
        {
            return $"Actions to card {Card.Name}:";
        }
    }
}