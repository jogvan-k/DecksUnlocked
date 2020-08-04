using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class RemoveStun : UseCreature
  {
    public RemoveStun(Creature creature, bool allowOutOfHouseUse = false) : base(creature, allowOutOfHouseUse)
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

    bool Equals(RemoveStun other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is RemoveStun other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 21 * Creature.GetHashCode();
    }
  }
}