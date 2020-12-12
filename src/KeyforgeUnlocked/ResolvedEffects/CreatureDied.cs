using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureDied : ResolvedEffectWithCreature<CreatureDied>
  {

    public CreatureDied(Creature creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} died";
    }
  }
}