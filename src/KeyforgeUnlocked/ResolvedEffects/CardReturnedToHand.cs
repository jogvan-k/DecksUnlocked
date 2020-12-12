using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardReturnedToHand : IResolvedEffect
  {
    public readonly Card Card;

    public CardReturnedToHand(Card card)
    {
      Card = card;
    }

    bool Equals(CardReturnedToHand other)
    {
      return Equals(Card, other.Card);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CardReturnedToHand other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(GetType(), Card);
    }

    public override string ToString()
    {
      return $"{Card.Name} returned to hand";
    }
  }
}