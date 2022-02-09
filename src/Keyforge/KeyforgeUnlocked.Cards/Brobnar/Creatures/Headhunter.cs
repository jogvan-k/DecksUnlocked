using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  [CardInfo("Headhunter", Rarity.Common,
    "Fight: Gain 1 æmber",
    "\"I mean, I think it's a head...\"")]
  [ExpansionSet(Expansion.CotA, 35)]
  public class Headhunter : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Giant};
    static readonly Callback FightAbility = (s, _, _) => s.GainAember();

    public Headhunter() : this(House.Brobnar)
    {
    }

    public Headhunter(House house) : base(house, Power, Armor, Traits, fightAbility: FightAbility)
    {
    }
  }
}