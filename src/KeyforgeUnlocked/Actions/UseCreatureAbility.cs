using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class UseCreatureAbility : UseCreature
  {
    public UseCreatureAbility(Creature creature, bool allowOutOfHouseUse = false) : base(creature, allowOutOfHouseUse)
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
      state.Effects.Push(new CreatureAbility(Creature));
    }

    bool Equals(UseCreatureAbility other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is UseCreatureAbility other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 7 * Creature.GetHashCode();
    }
  }
}