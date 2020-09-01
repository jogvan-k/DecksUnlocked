using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class CreatureAbility : UseCreature
  {
    public CreatureAbility(Creature creature) : base(creature)
    {
    }

    protected override void SpecificResolve(MutableState state, Creature creature)
    {
      creature.Card.CreatureAbility(state, creature.Id);
    }

    bool Equals(CreatureAbility other)
    {
      return Creature.Equals(other.Creature);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is CreatureAbility other && Equals(other);
    }

    public override int GetHashCode()
    {
      return Creature.GetHashCode();
    }
  }
}