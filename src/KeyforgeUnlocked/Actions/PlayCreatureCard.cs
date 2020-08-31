using System;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayCreatureCard : BasicAction
  {
    public CreatureCard Card { get; }
    public int Position { get; }

    public PlayCreatureCard(
      ImmutableState origin,
      CreatureCard card,
      int position) : base(origin)
    {
      Card = card;
      Position = position;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);

      state.Effects.Push(
        new Effects.PlayCreatureCard(
          Card,
          Position));
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PlayCreatureCard) obj);
    }

    bool Equals(PlayCreatureCard other)
    {
      return Equals(Card, other.Card) && Position == other.Position;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Card, Position);
    }
  }
}