using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class DiscardCard : BasicAction
  {
    public ICard Card { get; }

    public DiscardCard(ImmutableState origin, ICard card) : base(origin)
    {
      Card = card;
    }

    protected override void DoSpecificActionNoResolve(MutableState state)
    {
      state.Effects.Push(new Effects.DiscardCard(Card));
    }

    public override string Identity()
    {
      return Card.Id;
    }

    protected override bool Equals(BasicAction other)
    {
      return Equals(Card, ((DiscardCard)other).Card);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card);
    }
  }
}