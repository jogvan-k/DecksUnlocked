using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public class SampleArtifactCard : Card, IArtifactCard
  {
    public ArtifactType[] CardTypes { get; }

    public Callback CardActionAbility { get; }

    public SampleArtifactCard(
      House house = House.Undefined,
      ArtifactType[] types = null,
      Pip[] pips = null,
      Callback actionAbility = null,
      Callback playAbility = null,
      string id = null) : base(house, pips, playAbility, id)
    {
      CardTypes = types ?? new ArtifactType[0];
      CardActionAbility = actionAbility;
    }
  }
}