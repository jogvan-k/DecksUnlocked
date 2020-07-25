using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturesSwapped : ResolvedEffectWithTwoCreatures
  {
    public CreaturesSwapped(Creature creature, Creature target) : base(creature, target)
    {
    }

    public override int GetHashCode()
    {
      return 2 * base.GetHashCode();
    }
  }
}