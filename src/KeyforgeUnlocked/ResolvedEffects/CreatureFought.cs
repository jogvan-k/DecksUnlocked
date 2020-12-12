using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureFought : ResolvedEffectWithTwoCreatures<CreatureFought>
  {
    public CreatureFought(Creature fighter,
      Creature target) : base(fighter, target)
    {
    }

    public override string ToString()
    {
      return
        $"{Creature.Card.Name} (power: {Creature.Power}) attacked {Target.Card.GetType().Name} (power: {Target.Power})";
    }
  }
}