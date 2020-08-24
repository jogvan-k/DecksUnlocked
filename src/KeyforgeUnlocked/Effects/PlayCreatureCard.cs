using System;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayCreatureCard : IEffect
  {
    public readonly CreatureCard Card;
    public readonly int Position;

    public PlayCreatureCard(
      CreatureCard card,
      int position)
    {
      Card = card;
      Position = position;
    }

    public void Resolve(MutableState state)
    {
      ValidatePosition(state);
      var creature = Card.InsantiateCreature();
      state.Fields[state.PlayerTurn].Insert(Position, creature);
      state.ResolvedEffects.Add(new CreaturePlayed(creature, Position));

      Card.PlayAbility?.Invoke(state, Card.Id);
    }

    void ValidatePosition(IState state)
    {
      var creaturesOnField = state.Fields[state.PlayerTurn].Count;
      if (!(0 <= Position && Position <= creaturesOnField))
        throw new InvalidBoardPositionException(state, Position);
    }

    bool Equals(PlayCreatureCard other)
    {
      return Equals(Card, other.Card) && Position == other.Position;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is PlayCreatureCard other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Card, Position);
    }
  }
}