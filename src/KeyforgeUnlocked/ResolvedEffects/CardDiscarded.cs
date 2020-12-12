using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardDiscarded : IResolvedEffect
  {
    public Card Card { get; }

    public CardDiscarded(Card card)
    {
      Card = card;
    }

    bool Equals(CardDiscarded other)
    {
      return Equals(Card, other.Card);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CardDiscarded other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(GetType(), Card);
    }

    public override string ToString()
    {
      return $"Discarded {Card}";
    }
  }
}