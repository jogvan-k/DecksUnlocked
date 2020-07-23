using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class UseCreature : BasicAction
  {
    public readonly Creature Creature;

    public UseCreature(Creature creature)
    {
      Creature = creature;
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
      if (Creature.Card.House != house)
        throw new NotFromActiveHouseException(state, Creature.Card, house ?? House.Undefined);
    }
  }
}