using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class Reap : UseCreature<Reap>
  {
    public Reap(ImmutableState origin, Creature creature, bool allowOutOfHouseUse = false) : base(origin, creature, allowOutOfHouseUse)
    {
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      if(Creature.IsStunned())
        throw new CreatureStunnedException(state, Creature);
    }

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      state.Effects.Push(new Effects.Reap(Creature));
    }

    public override string ToString()
    {
      return "Reap creature";
    }
  }
}