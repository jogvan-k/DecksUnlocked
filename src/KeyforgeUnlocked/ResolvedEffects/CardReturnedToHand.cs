using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardReturnedToHand : IResolvedEffect
  {
    public readonly Card Card;

    public CardReturnedToHand(Card card)
    {
      this.Card = card;
    }
  }
}