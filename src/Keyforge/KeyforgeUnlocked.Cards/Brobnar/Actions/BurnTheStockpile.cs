using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
    [CardInfo("Burn the Stockpile", Rarity.Uncommon,
        "Play: If your opponent has 7 æmber or more, they lose 4 æmber.",
        "\"If you can't protect it, you don't deserve it.\" -Bilgum Avalanche")]
    [ExpansionSet(Expansion.CotA, 5)]
    [ExpansionSet(Expansion.AoA, 19)]
    public sealed class BurnTheStockpile : ActionCard
    {
        static readonly Callback PlayAbility = (s, _, p) =>
        {
            if (s.Aember[p.Other()] >= 7) s.LoseAember(p.Other(), 4);
        };

        public BurnTheStockpile() : this(House.Brobnar)
        {
        }

        public BurnTheStockpile(House house) : base(house, playAbility: PlayAbility)
        {
        }
    }
}