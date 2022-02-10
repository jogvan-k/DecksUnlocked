using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
    [CardInfo("Krump", Rarity.Common,
        "After an enemy creature is destroyed fighting Krump, its controller loses 1 æmber.")]
    [ExpansionSet(Expansion.CotA, 39)]
    public sealed class Krump : CreatureCard
    {
        const int Power = 6;
        const int Armor = 0;
        static readonly Trait[] Types = { Trait.Giant };

        static readonly Callback AfterKillAbility = (s, _, p) => { s.LoseAember(p.Other()); };

        public Krump() : this(House.Brobnar)
        {
        }

        public Krump(House house) : base(house, Power, Armor, Types, afterKillAbility: AfterKillAbility)
        {
        }
    }
}