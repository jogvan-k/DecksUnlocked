using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayCreatureCard : PlayCard<PlayCreatureCard>
  {
    public ICreatureCard CreatureCard => (ICreatureCard) Card;
    public readonly int Position;

    public PlayCreatureCard(ICreatureCard card, int position) : base(card)
    {
      Position = position;
    }

    protected override void ResolveImpl(MutableState state)
    {
      ValidatePosition(state);
      var creature = CreatureCard.InsantiateCreature();
      state.Fields[state.PlayerTurn].Insert(Position, creature);
      state.ResolvedEffects.Add(new CreaturePlayed(creature, Position));

      ResolvePlayEffects(state);
    }

    void ValidatePosition(IState state)
    {
      var creaturesOnField = state.Fields[state.PlayerTurn].Count;
      if (!(0 <= Position && Position <= creaturesOnField))
        throw new InvalidBoardPositionException(state, Position);
    }

    protected override bool Equals(PlayCreatureCard other)
    {
      return base.Equals(other) && Position.Equals(other.Position);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Position);
    }
  }
}