using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public sealed class PlayCreatureCardGroup : PlayCardGroup<PlayCreatureCardGroup>
    {
        public new ICreatureCard Card => (ICreatureCard)base.Card;

        public int BoardLength { get; }

        public PlayCreatureCardGroup(
            IState state,
            ICreatureCard card) : base(card)
        {
            BoardLength = state.Fields[state.PlayerTurn].Count;
            if (state == null || card == null)
                throw new ArgumentNullException();
        }

        protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
        {
            var list = ImmutableList<IAction>.Empty;

            var leftPosition = new PlayCreatureCard(origin, Card, 0);
            if (Card.CardPlayAllowed(origin, leftPosition))
                list = list.Add(leftPosition);

            if (BoardLength > 0)
            {
                var rightPosition = new PlayCreatureCard(origin, Card, BoardLength);
                if (Card.CardPlayAllowed(origin, rightPosition))
                    list = list.Add(rightPosition);
            }

            return list.Add(DiscardAction(origin));
        }

        public override string ToString()
        {
            return $"Actions to card {Card.Name}:";
        }
    }
}