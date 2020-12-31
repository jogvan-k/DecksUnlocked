using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
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