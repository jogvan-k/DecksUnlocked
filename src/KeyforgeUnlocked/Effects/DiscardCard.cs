using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DiscardCard : IEffect
  {
    public Card Card { get; }

    public DiscardCard(Card card)
    {
      Card = card;
    }

    public void Resolve(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);
      state.Discards[state.PlayerTurn].Add(Card);
      state.ResolvedEffects.Add(new ResolvedEffects.CardDiscarded(Card));
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DiscardCard) obj);
    }

    bool Equals(DiscardCard other)
    {
      return Equals(Card, other.Card);
    }

    public override int GetHashCode()
    {
      return (Card != null ? Card.GetHashCode() : 0);
    }
  }
}