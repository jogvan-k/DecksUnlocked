using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public interface IArtifactCard : ICard
  {
    ArtifactType[] CardTypes { get; }
    Callback CardActionAbility { get; }
  }
}