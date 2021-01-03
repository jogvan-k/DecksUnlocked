using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardInfo("Dew Faerie",
    Rarity.Common,
    "Reap: Gain 1 æmber.")]
  [ExpansionSet(Expansion.CotA, 350)]
  public class DewFaerie : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Faerie};
    static readonly Callback reapAbility = (s, _, _) => s.GainAember();

    public DewFaerie() : this(House.Untamed)
    {
    }
    
    public DewFaerie(House house) : base(house, Power, Armor, Traits, reapAbility: reapAbility)
    {
    }
  }
}