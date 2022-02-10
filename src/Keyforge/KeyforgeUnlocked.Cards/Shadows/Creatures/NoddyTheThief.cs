using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
    [CardInfo("Noddy the Thief",
        Rarity.Common,
        "Action: Steal 1 æmber.")]
    [ExpansionSet(Expansion.CotA, 306)]
    public sealed class NoddyTheThief : CreatureCard
    {
        const int Power = 2;
        const int Armor = 0;
        static readonly Trait[] Traits = { Trait.Elf, Trait.Thief };
        static readonly Keyword[] Keywords = { Keyword.Elusive };
        static readonly Callback CreatureAbility = (s, _, p) => s.StealAember(p);

        public NoddyTheThief() : this(House.Shadows)
        {
        }

        public NoddyTheThief(House house) : base(
            house, Power, Armor, Traits, Keywords, creatureAbility: CreatureAbility)
        {
        }
    }
}