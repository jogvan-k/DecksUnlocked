using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayActionCard : Action
  {
    public readonly ActionCard Card;

    public PlayActionCard(ActionCard card)
    {
      Card = card;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);

      state.Effects.Push(new Effects.PlayActionCard(Card));
    }

    bool Equals(PlayActionCard other)
    {
      return Equals(Card, other.Card);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is PlayActionCard other && Equals(other);
    }

    public override int GetHashCode()
    {
      return (Card != null ? Card.GetHashCode() : 0);
    }
  }
}