using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public abstract class PlayCardGroup<T> : ActionGroupBase<T> where T : PlayCardGroup<T>
    {
        public ICard Card { get; }

        protected PlayCardGroup(ICard card)
        {
            Card = card;
        }

        protected IAction DiscardAction(ImmutableState origin)
        {
            return new DiscardCard(origin, Card);
        }

        protected override bool Equals(T other)
        {
            return Equals(Card, other.Card);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Card);
        }
    }
}