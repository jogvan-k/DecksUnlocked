using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardReturnedToHand : ResolvedEffectWithCard<CardReturnedToHand>
  {
    public readonly Card Card;

    public CardReturnedToHand(Card card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"{Card.Name} returned to hand";
    }
  }
}