using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayActionCard : IEffect
  {
    public readonly ActionCard Card;

    public PlayActionCard(ActionCard card)
    {
      Card = card;
    }

    public void Resolve(MutableState state)
    {
      state.ResolvedEffects.Add(new ActionPlayed(Card));
      Card.PlayAbility?.Invoke(state, Card.Id);
      state.Discards[state.playerTurn].Add(Card);
    }

    bool Equals(PlayActionCard other)
    {
      return Equals(Card, other.Card);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is PlayActionCard other && Equals(other);
    }

    public override int GetHashCode()
    {
      return (Card != null ? Card.GetHashCode() : 0);
    }
  }
}