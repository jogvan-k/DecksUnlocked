using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class UseCreature : BasicAction
  {
    public readonly Creature Creature;
    readonly bool _allowOutOfHouseUse;

    public UseCreature(ImmutableState origin, Creature creature, bool allowOutOfHouseUse) : base(origin)
    {
      Creature = creature;
      _allowOutOfHouseUse = allowOutOfHouseUse;
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      ValidateActiveHouse(state);
      ValidateCreatureIsReady(state);
    }

    void ValidateCreatureIsReady(IState state)
    {
      if (!Creature.IsReady)
        throw new CreatureNotReadyException(state, Creature);
    }

    void ValidateActiveHouse(IState state)
    {
      var house = state.ActiveHouse;
      if (!_allowOutOfHouseUse && Creature.Card.House != house)
        throw new NotFromActiveHouseException(state, Creature.Card, house ?? House.Undefined);
    }

    protected override bool Equals(BasicAction other)
    {
      return Equals(Creature, ((UseCreature)other).Creature);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Creature);
    }
  }
}