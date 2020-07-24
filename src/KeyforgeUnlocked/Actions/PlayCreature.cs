using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayCreature : BasicAction
  {
    public CreatureCard Card { get; }
    public int Position { get; }

    public PlayCreature(
      CreatureCard card,
      int position)
    {
      Card = card;
      Position = position;
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Push(
        new Effects.PlayCreature(
          Card,
          Position));
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PlayCreature) obj);
    }

    bool Equals(PlayCreature other)
    {
      return Equals(Card, other.Card) && Position == other.Position;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Card, Position);
    }
  }
}