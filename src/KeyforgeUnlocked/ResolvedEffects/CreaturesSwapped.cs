using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturesSwapped : ResolvedEffectWithTwoCreatures
  {
    public CreaturesSwapped(Creature creature, Creature target) : base(creature, target)
    {
    }

    public override string ToString()
    {
      return $"{Creature.Card.Name} swapped position with {Target.Card.Name}";
    }
  }
}