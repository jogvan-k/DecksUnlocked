using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
    [CardInfo("Francus", Rarity.Uncommon,
        "After an enemy creature is destroyed fighting Francus, Francus captures 1 æmber.")]
    [ExpansionSet(Expansion.CotA, 243)]
    public sealed class Francus : CreatureCard
    {
        const int Power = 6;
        const int Armor = 1;
        static readonly Trait[] Types = { Trait.Knight, Trait.Spirit };
        static readonly Callback AfterKillAbility = (s, i, _) => { s.CaptureAember(i); };

        public Francus() : this(House.Sanctum)
        {
        }

        public Francus(House house) : base(house, Power, Armor, Types, afterKillAbility: AfterKillAbility)
        {
        }
    }
}