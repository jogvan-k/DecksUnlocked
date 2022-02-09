using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreatureFought : ResolvedEffectWithTwoIdentifiables<CreatureFought>
  {
    public CreatureFought(IIdentifiable fighter, Creature target) : base(fighter, target)
    {
    }

    public override string ToString()
    {
      return
        $"{Id.Name} attacked {Target.Name}";
    }
  }
}