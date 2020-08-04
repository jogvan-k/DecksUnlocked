using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class Reap : UseCreature
  {
    public Reap(Creature creature, bool allowOutOfHouseUse = false) : base(creature, allowOutOfHouseUse)
    {
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      if(Creature.IsStunned())
        throw new CreatureStunnedException(state, Creature);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Push(new Effects.Reap(Creature));
    }

    protected bool Equals(Reap other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Reap) obj);
    }

    public override int GetHashCode()
    {
      return 11 * Creature.GetHashCode();
    }
  }
}