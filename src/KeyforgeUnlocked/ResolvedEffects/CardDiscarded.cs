using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardDiscarded : ResolvedEffectWithCard<CardDiscarded>
  {
    public Card Card { get; }

    public CardDiscarded(Card card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"Discarded {Card}";
    }
  }
}