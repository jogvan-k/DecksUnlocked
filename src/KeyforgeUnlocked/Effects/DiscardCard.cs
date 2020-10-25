using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DiscardCard : EffectBase<DiscardCard>
  {
    public Card Card { get; }

    public DiscardCard(Card card)
    {
      Card = card;
    }

    protected override void ResolveImpl(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);
      state.Discards[state.PlayerTurn].Add(Card);
      state.ResolvedEffects.Add(new CardDiscarded(Card));
    }

    protected override bool Equals(DiscardCard other)
    {
      return Card.Equals(other.Card);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode() * Constants.PrimeHashBase + Card.GetHashCode();
    }
  }
}