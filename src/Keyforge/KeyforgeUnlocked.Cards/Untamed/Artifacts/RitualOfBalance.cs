using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Artifacts
{
  [CardInfo("Ritual of Balance", Rarity.Uncommon,
    "Action: If your opponent has 6 æmber or more, steal 1 æmber.",
    "Is balance a means to an end, or an end in itself?")]
  [ExpansionSet(Expansion.CotA, 342)]
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