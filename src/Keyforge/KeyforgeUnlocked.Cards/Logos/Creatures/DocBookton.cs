using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
    [CardInfo("Doc Bookton", Rarity.Common,
        "Reap: Draw a card.",
        "\"Don't worry, Momo. We'll have this quantum death ray installed in no time.\"")]
    [ExpansionSet(Expansion.CotA, 139)]
    public class DocBookton : CreatureCard
    {
        const int Power = 5;
        const int Armor = 0;

        static readonly Trait[] Traits =
        {
            Trait.Human, Trait.Scientist
        };

        static readonly Callback ReapAbility = (s, _, p) => s.Draw(p);

        public DocBookton() : this(House.Logos)
        {
        }

        public DocBookton(House house) : base(house, Power, Armor, Traits, reapAbility: ReapAbility)
        {
        }
    }
}