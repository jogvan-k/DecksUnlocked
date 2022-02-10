using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public class ArtifactUsed : ResolvedEffectWithIdentifiable<ArtifactUsed>
    {
        public ArtifactUsed(IIdentifiable artifact) : base(artifact)
        {
        }

        public override string ToString()
        {
            return $"{Id.Name} used";
        }
    }
}