using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  [CardInfo("Kelifi Dragon", Rarity.Rare,
    "Kelifi Dragon cannot be played unless you have 7 æmber or more.\nFight/Reap: Gain 1 æmber. Deal 5 damage to a creature.")]
  [ExpansionSet(Expansion.CotA, 37)]
  public class KelifiDragon : CreatureCard
  {
    const int Power = 12;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Dragon
    };

    static readonly ActionPredicate PlayAllowed = (s, _) => s.Aember[s.PlayerTurn] >= 7;

    static readonly Callback FightReapAbility = (s, _, _) =>
    {
      s.GainAember();
      s.AddEffect(new TargetSingleCreature((s1, t, _) => s1.DamageCreature(t, 5)));
    };

    public KelifiDragon() : this(House.Brobnar)
    {
    }

    public KelifiDragon(House house) : base(house, Power, Armor, Traits, fightAbility: FightReapAbility,
      reapAbility: FightReapAbility, playAllowed: PlayAllowed)
    {
    }
  }
}