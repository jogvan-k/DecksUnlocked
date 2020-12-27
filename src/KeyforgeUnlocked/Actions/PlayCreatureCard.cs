using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayCreatureCard : BasicAction
  {
    public ICreatureCard Card { get; }
    public int Position { get; }

    public PlayCreatureCard(
      ImmutableState origin,
      ICreatureCard card,
      int position) : base(origin)
    {
      Card = card;
      Position = position;
    }

    protected override void DoSpecificActionNoResolve(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card);

      state.Effects.Push(
        new Effects.PlayCreatureCard(
          Card,
          Position));
    }

    public override string Identity()
    {
      return Card.Id + Position;
    }

    protected override bool Equals(BasicAction other)
    {
      var otherAction = (PlayCreatureCard) other;
      return Equals(Card, otherAction.Card) && Position == otherAction.Position;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card, Position);
    }
  }
}