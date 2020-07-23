using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class CreatureAbility : IEffect
  {
    public Creature Creature;

    public CreatureAbility(Creature creature)
    {
      Creature = creature;
    }

    public void Resolve(MutableState state)
    {
      Creature.Card.CreatureAbility(state);
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