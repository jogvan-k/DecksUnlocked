using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardDiscarded : ResolvedEffectWithCard<CardDiscarded>
  {
    public ICard Card { get; }

    public CardDiscarded(ICard card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"Discarded {Card}";
    }
  }
}