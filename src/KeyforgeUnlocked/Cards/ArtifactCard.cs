using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public class ArtifactCard : Card, IArtifactCard
  {
    public ArtifactType[] CardTypes { get; }
    public Callback CardActionAbility { get; }

    public ArtifactCard(House house, ArtifactType[] types, Pip[] pips = null, Callback playAbility = null, Callback actionAbility = null) : base(house, pips, playAbility)
    {
      CardTypes = types ?? new ArtifactType[0];
      CardActionAbility = actionAbility;
    }
  }
}