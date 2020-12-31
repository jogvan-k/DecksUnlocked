using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
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