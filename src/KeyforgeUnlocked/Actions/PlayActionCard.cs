using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayActionCard : Action<PlayActionCard>
  {
    public readonly ActionCard Card;

    public PlayActionCard(ImmutableState origin, ActionCard card) : base(origin)
    {
      Card = card;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);

      state.Effects.Push(new Effects.PlayActionCard(Card));
    }

    public override string Identity()
    {
      return Card.Id;
    }

    protected override bool Equals(PlayActionCard other)
    {
      return Equals(Card, other.Card);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card);
    }
  }
}