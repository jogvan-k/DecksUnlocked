using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
    [CardInfo("Sanctum Guardian", Rarity.Rare,
        "Fight/Reap: Swap Sanctum Guardian with another friendly creature in your battleline.")]
    [ExpansionSet(Expansion.CotA, 256)]
    [ExpansionSet(Expansion.AoA, 265)]
    public sealed class SanctumGuardian : CreatureCard
    {
        const int power = 6;
        const int armor = 1;
        static readonly Trait[] Traits = { Trait.Knight, Trait.Spirit };
        static readonly Keyword[] Keywords = { Keyword.Taunt };

        static readonly Callback FightReapAbility = (s, self, p) =>
        {
            s.AddEffect(new TargetSingleCreature(Delegates.SwapCreatures(self), Target.Own, Delegates.AlliesOf(p)));
        };

        public SanctumGuardian() : this(House.Sanctum)
        {
        }

        public SanctumGuardian(House house) : base(
            house, power, armor, Traits, Keywords, fightAbility: FightReapAbility, reapAbility: FightReapAbility)
        {
        }
    }
}