using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayCreatureCard : EffectBase<PlayCreatureCard>
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

    protected override void ResolveImpl(MutableState state)
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

    protected override bool Equals(PlayCreatureCard other)
    {
      return Card.Equals(other.Card) && Position.Equals(other.Position);
    }

    public override int GetHashCode()
    {
      var hash = base.GetHashCode();
      hash = hash * Constants.PrimeHashBase + Card.GetHashCode();
      hash = hash * Constants.PrimeHashBase + Position.GetHashCode();
      
      return hash;
    }
  }
}