using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class DiscardCard : BasicAction
  {
    public Card Card { get; }

    public DiscardCard(Card card)
    {
      Card = card;
    }

    internal override MutableState DoActionNoResolve(MutableState state)
    {
      Validate(state);
      state.Effects.Enqueue(new Effects.DiscardCard(Card));
      return state;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is DiscardCard other && Equals(other);
    }

    bool Equals(DiscardCard other)
    {
      return Equals(Card, other.Card);
    }

    public override int GetHashCode()
    {
      return (Card != null ? Card.GetHashCode() : 0);
    }
  }
}