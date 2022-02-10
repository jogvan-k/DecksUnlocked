using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
    [CardInfo("Bad Penny", Rarity.Common,
        "Destroyed: Return Bad Penny to your hand.",
        "A Bad Penny saved is a Bad Penny earned.")]
    [ExpansionSet(Expansion.CotA, 296)]
    [ExpansionSet(Expansion.AoA, 267)]
    [ExpansionSet(Expansion.WC, 326)]
    public sealed class BadPenny : CreatureCard
    {
        const int Power = 1;
        const int Armor = 0;
        static readonly Trait[] Traits = { Trait.Human, Trait.Thief };
        static readonly Callback DestroyedAbility = (s, id, _) => s.ReturnFromDiscard(id);

        public BadPenny() : this(House.Shadows)
        {
        }

        public BadPenny(House house) : base(house, Power, Armor, Traits, destroyedAbility: DestroyedAbility)
        {
        }
    }
}