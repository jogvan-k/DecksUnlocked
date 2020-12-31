using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public class ArtifactCard : Card, IArtifactCard
  {
    public Trait[] CardTraits { get; }
    public Callback CardActionAbility { get; }

    public ArtifactCard(House house, Trait[] traits, Pip[] pips = null, Callback playAbility = null, Callback actionAbility = null) : base(house, pips, playAbility)
    {
      CardTraits = traits ?? new Trait[0];
      CardActionAbility = actionAbility;
    }
  }
}