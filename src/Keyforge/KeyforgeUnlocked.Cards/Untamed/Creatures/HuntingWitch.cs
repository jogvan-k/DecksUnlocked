﻿using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
    [CardInfo("Hunting Witch",
        Rarity.Common,
        "Each time you play another creature, gain 1 æmber.",
        "\"What is it? Is it food?\"")]
    [ExpansionSet(Expansion.CotA, 367)]
    public class HuntingWitch : CreatureCard
    {
        const int Power = 2;
        const int Armor = 0;

        static readonly Trait[] Traits =
        {
            Trait.Human, Trait.Witch
        };

        static readonly Callback PlayAbility = (s, i, p) =>
        {
            s.Events.SubscribeUntilLeavesPlay(i, EventType.CardPlayed, (s, c, pc) =>
            {
                if (p == pc && c is ICreatureCard)
                    s.GainAember(pc);
            });
        };

        public HuntingWitch() : this(House.Untamed)
        {
        }

        public HuntingWitch(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
        {
        }
    }
}