using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public abstract class ResolvedEffectWithCard : IResolvedEffect
  {
    protected readonly Card _card;

    protected ResolvedEffectWithCard(Card card)
    {
      _card = card;
    }

    protected bool Equals(ResolvedEffectWithCard other)
    {
      return Equals(_card, other._card);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ResolvedEffectWithCard) obj);
    }

    public override int GetHashCode()
    {
      return (_card != null ? _card.GetHashCode() : 0);
    }
  }
}