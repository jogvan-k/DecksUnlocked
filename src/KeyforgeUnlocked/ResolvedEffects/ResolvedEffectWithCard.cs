using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithCard<T> : Equatable<T>, IResolvedEffect where T : ResolvedEffectWithCard<T>
  {
    protected readonly ICard _card;

    protected ResolvedEffectWithCard(ICard card)
    {
      _card = card;
    }

    protected override bool Equals(T other)
    {
      return Equals(_card, other._card);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), _card);
    }
  }
}