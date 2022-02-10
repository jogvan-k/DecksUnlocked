using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Artifacts
{
    public static class ArtifactDtoExtensions
    {
        public static ArtifactDto ToDto(this Artifact artifact) =>
            new()
            {
                Card = artifact.Card.ToDto(),
                Id = artifact.Id,
                IsReady = artifact.IsReady
            };

        public static Artifact ToArtifact(this ArtifactDto dto) => new((IArtifactCard)dto.Card.ToCard(), dto.IsReady);
    }
}