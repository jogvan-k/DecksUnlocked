using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public abstract class UseCreature : BasicAction
  {
    public string CreatureId;

    public UseCreature(string creatureId)
    {
      CreatureId = creatureId;
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      CreatureUtil.FindAndValidateCreatureReady(state, CreatureId, out var creature);
      ValidateActiveHouse(creature, state);
    }

    void ValidateActiveHouse(Creature creature,
      IState state)
    {
      var house = state.ActiveHouse;
      if (creature.Card.House != house)
        throw new NotFromActiveHouseException(state, creature.Card, house ?? House.Undefined);
    }
  }
}