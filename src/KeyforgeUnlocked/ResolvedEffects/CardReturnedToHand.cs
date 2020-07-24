using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CardReturnedToHand : IResolvedEffect
  {
    public Card card { get; }

    public CardReturnedToHand(Card card)
    {
      this.card = card;
    }
  }
}