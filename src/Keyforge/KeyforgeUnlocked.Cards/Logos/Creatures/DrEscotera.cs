using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
    [CardInfo("Dr. Escotera", Rarity.Common,
        "Play: Gain 1 æmber for each forged key your opponent has.",
        "\"Interesting reaction, but what does it mean?\"")]
    [ExpansionSet(Expansion.CotA, 140)]
    public class DrEscotera : CreatureCard
    {
        const int Power = 4;
        const int Armor = 0;
        static readonly Trait[] Traits = { Trait.Cyborg, Trait.Scientist };

        static readonly Callback PlayAbility = (s, _, p) =>
        {
            var opponentKeys = s.Keys[p.Other()];
            s.GainAember(p, opponentKeys);
        };

        public DrEscotera() : this(House.Logos)
        {
        }

        public DrEscotera(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
        {
        }
    }
}