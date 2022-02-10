using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
    [CardInfo("Mother", Rarity.Common,
        "During your \"draw cards\" step, refill your hand to 1 additional card.",
        "\"Of course she's neccessary, she's the mother of all invention!\"")]
    [ExpansionSet(Expansion.CotA, 145)]
    public class Mother : CreatureCard
    {
        const int Power = 5;
        const int Armor = 0;

        static readonly Trait[] Traits =
        {
            Trait.Robot, Trait.Scientist
        };

        static readonly Callback PlayAbility = (s, i, p) =>
        {
            s.Events.SubscribeUntilLeavesPlay(i, ModifierType.HandLimit, s1 => s1.PlayerTurn == p ? 1 : 0);
        };

        public Mother() : this(House.Logos)
        {
        }

        public Mother(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
        {
        }
    }
}