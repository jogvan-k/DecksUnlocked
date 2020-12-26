using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class CreaturesSwapped : ResolvedEffectWithTwoIdentifiables<CreaturesSwapped>
  {
    public CreaturesSwapped(IIdentifiable creature, IIdentifiable target) : base(creature, target)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} swapped position with {Target.Name}";
    }
  }
}