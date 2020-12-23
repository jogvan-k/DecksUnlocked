using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class AemberCaptured : ResolvedEffectWithCreatureAndInt<AemberCaptured>
  {
    public AemberCaptured(Creature creature, int captured) : base(creature, captured)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} captured {Int} Aember";
    }
  }
}