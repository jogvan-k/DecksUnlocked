using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class BasicActionWithCard<T> : BasicAction<T> where T : BasicActionWithCard<T>
  {
    public readonly ICard Card;

    public BasicActionWithCard(ImmutableState originState, ICard card) : base(originState)
    {
      Card = card;
    }

    public override string Identity()
    {
      return Card.Id;
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