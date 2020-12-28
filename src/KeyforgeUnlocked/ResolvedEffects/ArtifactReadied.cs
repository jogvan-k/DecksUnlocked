using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class ArtifactReadied : ResolvedEffectWithIdentifiable<ArtifactReadied>
  {
    public ArtifactReadied(IIdentifiable id) : base(id)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} readied";
    }
  }
}