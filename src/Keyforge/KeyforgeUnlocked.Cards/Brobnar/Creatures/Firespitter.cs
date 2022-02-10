using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
    [CardInfo("Firespitter", Rarity.Common,
        "Before Fight: Deal 1 damage to each enemy creature.",
        "Guess how he got that name.")]
    [ExpansionSet(Expansion.CotA, 32)]
    public class Firespitter : CreatureCard
    {
        const int Power = 5;
        const int Armor = 1;

        static readonly Trait[] Traits =
        {
            Trait.Giant
        };

        static readonly Callback BeforeFightAbility = (s, _, _) =>
        {
            var effect = new TargetAllCreatures(
                (s, t, _) => s.DamageCreature(t), Delegates.EnemiesOf(s.PlayerTurn));
            effect.Resolve(s);
        };

        public Firespitter() : this(House.Brobnar)
        {
        }

        public Firespitter(House house) : base(house, Power, Armor, Traits, beforeFightAbility: BeforeFightAbility)
        {
        }
    }
}