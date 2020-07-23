using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class UseCreatureAbility : UseCreature
  {
    public UseCreatureAbility(Creature creature) : base(creature)
    {
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