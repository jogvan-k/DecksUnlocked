using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  [CardInfo("Macis Asp", Rarity.Uncommon)]
  [ExpansionSet(Expansion.CotA, 301)]
  [ExpansionSet(Expansion.MM, 268)]
  public sealed class MacisAsp : CreatureCard
  {
    const int Power = 3;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Beast};
    static readonly Keyword[] Keywords = {Keyword.Skirmish, Keyword.Poison};

    public MacisAsp() : this(House.Shadows)
    {
    }

    public MacisAsp(House house) : base(house, Power, Armor, Traits, Keywords)
    {
    }
  }
}