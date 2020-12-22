using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Effects
{
  public abstract class EffectWithCard<T> : EffectBase<T> where T : EffectWithCard<T>
  {
    public readonly Card Card;

    protected EffectWithCard(Card card)
    {
      Card = card;
    }
    
    protected override bool Equals(T other)
    {
      return Card.Equals(other.Card);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card);
    }
  }
}