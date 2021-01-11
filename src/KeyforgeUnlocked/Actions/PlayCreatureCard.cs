using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayCreatureCard : BasicActionWithCard<PlayCreatureCard>
  {
    public int Position { get; }

    public PlayCreatureCard(
      ImmutableState origin,
      ICreatureCard card,
      int position) : base(origin, card)
    {
      Position = position;
    }

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card);

      state.Effects.Push(
        new Effects.PlayCreatureCard(
          (ICreatureCard) Card,
          Position));
    }

    public override string Identity()
    {
      return Card.Id + Position;
    }

    protected override bool Equals(PlayCreatureCard other)
    {
      return base.Equals(other) &&  Position == other.Position;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card, Position);
    }

    public override string ToString()
    {
      return $"Play to position {Position}";
    }
  }
}