using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class ArtifactDestroyed : ResolvedEffectWithIdentifiable<ArtifactDestroyed>
  {
    public ArtifactDestroyed(IIdentifiable id) : base(id)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} destroyed";
    }
  }
}