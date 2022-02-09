using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using static KeyforgeUnlocked.Cards.Attributes.Expansion;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardInfo("Bigtwig",
    Rarity.Uncommon,
    "Bigtwig can only fight stunned creatures.\nReap: Stun and exhaust a creature.")]
  [ExpansionSet(CotA, 346)]
  public class Bigtwig : CreatureCard
  {
    const int Power = 7;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Beast
    };

    static readonly Callback ReapAbility = (s, _, _) =>
    {
      s.AddEffect(new TargetSingleCreature(
        (s, t, _) =>
        {
          s.StunCreature(t);
          s.ExhaustCreature(t);
        }));
    };

    static readonly ActionPredicate ActionAllowed = (_, a) =>
    {
      if (a is FightCreature f)
      {
        return f.Target.IsStunned();
      }

      return true;
    };

    public Bigtwig() : this(House.Untamed)
    {
    }

    public Bigtwig(House house) : base(house, Power, Armor, Traits, reapAbility: ReapAbility,
      useActionAllowed: ActionAllowed)
    {
    }
  }
}