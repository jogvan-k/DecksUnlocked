using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureFought : ResolvedEffectWithTwoCreatures
  {
    public CreatureFought(Creature fighter,
      Creature target) : base(fighter, target)
    {
    }

    public override int GetHashCode()
    {
      return 3 * base.GetHashCode();
    }
  }
}