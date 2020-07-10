using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayCreature : Effect
  {
    public readonly CreatureCard Card;
    public readonly int Position;

    public PlayCreature(
      CreatureCard card,
      int position)
    {
      Card = card;
      Position = position;
    }

    public override void Resolve(MutableState state)
    {
      ValidatePosition(state);
      state.Fields[state.PlayerTurn].Insert(Position, Card.InsantiateCreature());

      if(!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state);
    }

    void ValidatePosition(IState state)
    {
      var creaturesOnField = state.Fields[state.PlayerTurn].Count;
      if (!(0 <= Position && Position <= creaturesOnField))
        throw new InvalidBoardPositionException(state, Position);
    }

    bool Equals(PlayCreature other)
    {
      return Equals(Card, other.Card) && Position == other.Position;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is PlayCreature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Card, Position);
    }
  }
}