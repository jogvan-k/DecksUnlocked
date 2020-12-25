using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardReturnedToHand : ResolvedEffectWithCard<CardReturnedToHand>
  {

    public CardReturnedToHand(ICard card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"{_card} returned to hand";
    }
  }
}