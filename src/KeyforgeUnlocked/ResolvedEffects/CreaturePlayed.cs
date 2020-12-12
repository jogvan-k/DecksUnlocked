using System;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturePlayed : ResolvedEffectWithCreature<CreaturePlayed>
  {
    public readonly int Position;

    public CreaturePlayed(Creature creature,
      int position) : base(creature)
    {
      Position = position;
    }

    protected override bool Equals(CreaturePlayed other)
    {
      return base.Equals(other) && Position == other.Position;
    }


    public override string ToString()
    {
      return $"Played {Creature.Card.Name} on position {Position}";
    }
  }
}