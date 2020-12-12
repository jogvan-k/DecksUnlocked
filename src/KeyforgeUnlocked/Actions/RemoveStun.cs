using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class RemoveStun : UseCreature
  {
    public RemoveStun(ImmutableState origin, Creature creature, bool allowOutOfHouseUse = false) : base(origin, creature, allowOutOfHouseUse)
    {
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      if (!Creature.IsStunned())
        throw new CreatureNotStunnedException(state, Creature);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Enqueue(new Effects.RemoveStun(Creature));
    }
  }
}