using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
    [CardInfo("Smaaash", Rarity.Common,
        "Stun a creature.",
        "\"I'm not sure he knows any other words.\"")]
    [ExpansionSet(Expansion.CotA, 46)]
    public sealed class Smaaash : CreatureCard
    {
        const int Power = 5;
        const int Armor = 0;
        static readonly Trait[] Traits = { Trait.Giant };

        static readonly Callback PlayAbility =
            (s, _, _) => s.AddEffect(new TargetSingleCreature(Delegates.StunTarget));

        public Smaaash() : this(House.Brobnar)
        {
        }

        public Smaaash(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
        {
        }
    }
}