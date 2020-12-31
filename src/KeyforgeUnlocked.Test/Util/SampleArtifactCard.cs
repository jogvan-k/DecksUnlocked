using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public class SampleArtifactCard : Card, IArtifactCard
  {
    public Trait[] CardTraits { get; }

    public Callback CardActionAbility { get; }

    public SampleArtifactCard(
      House house = House.Undefined,
      Trait[] traits = null,
      Pip[] pips = null,
      Callback actionAbility = null,
      Callback playAbility = null,
      string id = null) : base(house, pips, playAbility, id)
    {
      CardTraits = traits ?? new Trait[0];
      CardActionAbility = actionAbility;
    }
  }
}