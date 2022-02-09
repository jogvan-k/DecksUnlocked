using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  [CardInfo("Umbra", Rarity.Common,
    "Fight: Steal 1 æmber.",
    "\"When the fightin's done, the real work begins\"")]
  public sealed class Umbra : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Elf, Trait.Thief};
    static readonly Keyword[] Keywords = {Keyword.Skirmish};
    static readonly Callback FightAbility = (s, _, p) => s.StealAember(p);

    public Umbra() : this(House.Shadows)
    {
    }

    public Umbra(House house) : base(
      house, Power,
      Armor, Traits, Keywords, fightAbility: FightAbility)
    {
    }
  }
}