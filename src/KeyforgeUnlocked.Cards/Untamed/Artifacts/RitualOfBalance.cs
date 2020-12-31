using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Artifacts
{
  [CardName("Ritual of Balance")]
  public class RitualOfBalance : ArtifactCard
  {
    static readonly Trait[] traits = {Trait.Power};

    static readonly Callback ActionAbility = (s, _, p) =>
    {
      if(s.Aember[p.Other()] >= 6)
        s.StealAember(p);
    };
    public RitualOfBalance() : this(House.Untamed)
    {
    }
    
    public RitualOfBalance(House house) : base(house, traits, actionAbility: ActionAbility)
    {
    }
  }
}