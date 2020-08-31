using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class DiscardCard : BasicAction
  {
    public Card Card { get; }

    public DiscardCard(ImmutableState origin, Card card) : base(origin)
    {
      Card = card;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Push(new Effects.DiscardCard(Card));
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