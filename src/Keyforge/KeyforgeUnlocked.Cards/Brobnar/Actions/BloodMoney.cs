using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
    [CardInfo("Blood Money", Rarity.Uncommon,
        "Play: Place 2 æmber from the common supply on an enemy creature.",
        "\"You! Æmber lover. You're next.\"")]
    [ExpansionSet(Expansion.CotA, 3)]
    [ExpansionSet(Expansion.AoA, 17)]
    [ExpansionSet(Expansion.WC, 17)]
    public sealed class BloodMoney : ActionCard
    {
        static readonly Callback PlayAbility =
            (s, _, _) =>
                s.AddEffect(new TargetSingleCreature((s, t, _) => s.AddAemberToCreature(t, 2), Target.Opponens));

        public BloodMoney() : this(House.Brobnar)
        {
        }

        public BloodMoney(House house) : base(house, playAbility: PlayAbility)
        {
        }
    }
}