using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardInfo("Lupo the Scarred",
    Rarity.Rare,
    "Play: Deal 2 damage to an enemy creature.",
    "\"Nothing that big should be able to move that silently.\" -Lost Lukas Lawrence")]
  [ExpansionSet(Expansion.CotA, 359)]
  [ExpansionSet(Expansion.AoA, 358)]
  public class LupoTheScarred : CreatureCard
  {
    const int Power = 6;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Beast
    };

    static readonly Keyword[] Keywords =
    {
      Keyword.Skirmish
    };

    static readonly Callback PlayAbility = (s, _, _) =>
    {
      var effect = new TargetSingleCreature(
        (s, t, _) => s.DamageCreature(t, 2), Target.Opponens);
      s.AddEffect(effect);
    };

    public LupoTheScarred() : this(House.Untamed)
    {
    }

    public LupoTheScarred(House house) : base(house, Power, Armor, Traits, keywords: Keywords, playAbility: PlayAbility)
    {
    }
  }
}